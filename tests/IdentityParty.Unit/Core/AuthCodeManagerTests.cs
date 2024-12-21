using AutoFixture;
using AutoFixture.Xunit;
using IdentityParty.Core;
using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Entities;
using Moq;

namespace IdentityParty.Unit.Core;

public class AuthCodeManagerTests
{
    private readonly Fixture _fixture = FixtureFactory.Create();

    [Fact]
    public async Task GetAuthCodeAsync_Always_ShouldReturnAuthorizationCode()
    {
        var authCodeIssuerMock = _fixture.Freeze<Mock<IAuthorizationCodeIssuer>>();
        authCodeIssuerMock.Setup(x => x.Issue())
            .Returns(_fixture.Create<string>());
        var grant = _fixture.Create<Grant>();
        var sut = _fixture.Create<AuthCodeManager>();

        var actual = await sut.AssignAuthCodeAsync(grant);

        Assert.False(string.IsNullOrEmpty(actual));
    }

    [Fact]
    public async Task GetAuthCodeAsync_Always_ShouldSaveAuthorizationCode()
    {
        var expectedAuthCode = _fixture.Create<string>();
        var authCodeIssuerMock = _fixture.Freeze<Mock<IAuthorizationCodeIssuer>>();
        authCodeIssuerMock.Setup(x => x.Issue())
            .Returns(expectedAuthCode);
        var grantStoreMock = _fixture.Freeze<Mock<IGrantStore>>();
        var grant = _fixture.Create<Grant>();
        var sut = _fixture.Create<AuthCodeManager>();

        _ = await sut.AssignAuthCodeAsync(grant);
        grantStoreMock.Verify(
            x => x.UpdateAsync(It.Is<Grant>(g => g.AuthorizationCode != null)),
            Times.Once);
    }

    [Theory, AutoData]
    public async Task ValidateCodeAsync_PassedCodeIsValid_ReturnsTrueAndSetAuthCodeAsUsed(
        [AutoFixture.Xunit2.Frozen] Mock<IGrantStore> grantStoreMock,
        Guid clientId,
        string passedCode,
        AuthCodeManager sut)
    {
        var expectedGrant = CreateValidGrant(expectedAuthCode: passedCode);
        grantStoreMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(expectedGrant);

        var actual = await sut.ValidateAuthorizationCodeAsync(passedCode, clientId);

        Assert.True(actual);
        Assert.True(expectedGrant.AuthCodeUsed);
    }

    [Theory]
    [AutoMoqData]
    public async Task ValidateCodeAsync_AuthCodeWasAlreadyUsed_ReturnsFalseAndRevokeAllTokens(
        [AutoFixture.Xunit2.Frozen] Mock<IAccessTokenManager> accessTokenManagerMock,
        [AutoFixture.Xunit2.Frozen] Mock<IIdTokenManager> idTokenManagerMock,
        [AutoFixture.Xunit2.Frozen] Mock<IGrantStore> grantStoreMock,
        string passedAuthCode,
        Guid clientId,
        AuthCodeManager sut)
    {
        var grantWithAlreadyUsedAuthCode = _fixture.Build<Grant>()
            .FromFactory(() => CreateValidGrant(passedAuthCode))
            .With(g => g.AuthCodeUsed, true).Create();
        grantStoreMock.Setup(x => x.GetAsync(clientId, passedAuthCode))
            .ReturnsAsync(grantWithAlreadyUsedAuthCode);

        var result = await sut.ValidateAuthorizationCodeAsync(passedAuthCode, clientId);

        Assert.False(result);
        idTokenManagerMock.Verify(x => x.CancelCurrent(), Times.Once);
        accessTokenManagerMock.Verify(x => x.CancelCurrent(), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public async Task ValidateCodeAsync_AuthCodeExpired_ReturnsFalse(
        [AutoFixture.Xunit2.Frozen] Mock<IGrantStore> grantStoreMock,
        string passedAuthCode,
        Guid clientId,
        AuthCodeManager sut)
    {
        var grantWithExpiredAuthorizationCode = _fixture.Build<Grant>()
            .FromFactory(() => CreateValidGrant(passedAuthCode))
            .With(g => g.AuthCodeUsed, true).Create();
        grantStoreMock.Setup(x => x.GetAsync(clientId, passedAuthCode))
            .ReturnsAsync(grantWithExpiredAuthorizationCode);

        var result = await sut.ValidateAuthorizationCodeAsync(passedAuthCode, clientId);

        Assert.False(result);
    }

    private Grant CreateValidGrant(string expectedAuthCode)
    {
        const int validExpTimeOffsetInMinutes = 10;
        var grant = _fixture.Build<Grant>()
            .With(x => x.AuthCodeUsed, false)
            .Create();
        grant.AssignAuthCode(expectedAuthCode);
        return grant;
    }
}