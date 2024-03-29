using AutoFixture;
using AutoFixture.Xunit2;
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
        authCodeIssuerMock.Setup(x => x.Generate(It.IsAny<Grant>()))
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
        authCodeIssuerMock.Setup(x => x.Generate(It.IsAny<Grant>()))
            .Returns(expectedAuthCode);
        var grantStoreMock = _fixture.Freeze<Mock<IGrantStore>>();
        var grant = _fixture.Create<Grant>();
        var sut = _fixture.Create<AuthCodeManager>();

        _ = await sut.AssignAuthCodeAsync(grant);
        grantStoreMock.Verify(
            x => x.Update(It.Is<Grant>(g => g.AuthorizationCode != null)),
            Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task ValidateCodeAsync_PassedCodeIsValid_ReturnsTrueAndSetAuthCodeAsUsed(
        [Frozen]Mock<IGrantStore> grantStoreMock,
        Guid clientId,
        string passedCode,
        AuthCodeManager sut
    )
    {
        var expectedGrant = CreateValidGrant(expectedAuthCode: passedCode);
        grantStoreMock.Setup(x => x.Get(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(expectedGrant);

        var actual = await sut.ValidateAuthorizationCodeAsync(passedCode, clientId);

        Assert.True(actual);
        Assert.True(expectedGrant.AuthCodeUsed);
    }

    [Fact]
    public async Task ValidateCodeAsync_AuthCodeWasAlreadyUsed_ShouldReturnFalseAndRevokeAllTokens()
    {
        
    }

    [Fact]
    public async Task ValidateCodeAsync_AuthCodeExpired_ReturnFalse()
    {
        
    }

    private Grant CreateValidGrant(string expectedAuthCode)
    {
        const int validExpTimeOffsetInMinutes = 10;   
        return _fixture.Build<Grant>()
            .With(x => x.AuthCodeExp, DateTime.Now.AddMinutes(validExpTimeOffsetInMinutes))
            .With(x => x.AuthCodeUsed, false)
            .With(x => x.AuthorizationCode, expectedAuthCode).Create();
    }
}
