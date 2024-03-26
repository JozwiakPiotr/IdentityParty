using AutoFixture;
using IdentityParty.Core;
using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Entities;
using Moq;

namespace IdentityParty.Unit.Core;

public class ClientManagerTests
{
    private readonly Fixture _fixture = FixtureFactory.Create();

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DoesClientExists_Always_ShouldReturnWetherClientExists(bool expected)
    {
        var clientStoreMock = _fixture.Freeze<Mock<IClientStore>>();
        var clientId = _fixture.Create<Guid>();
        clientStoreMock.Setup(x => x.Exists(clientId)).ReturnsAsync(expected);
        var sut = _fixture.Create<ClientManager>();

        var actual = await sut.DoesClientExistAsync(clientId);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetAuthCodeAsync_Always_ShouldReturnAuthorizationCode()
    {
        var authCodeIssuerMock = _fixture.Freeze<Mock<IAuthorizationCodeIssuer>>();
        authCodeIssuerMock.Setup(x => x.Generate(It.IsAny<Grant>()))
            .Returns(_fixture.Create<string>());
        var grant = _fixture.Create<Grant>();
        var sut = _fixture.Create<ClientManager>();

        var actual = await sut.GetAuthCodeAsync(grant);

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
        var sut = _fixture.Create<ClientManager>();

        _ = sut.GetAuthCodeAsync(grant);
        grantStoreMock.Verify(
            x => x.Update(It.Is<Grant>(g => g.AuthorizationCode != null)),
            Times.Once);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task IsClientGrantedAsync_Always_ShouldReturnWetherClientIsGranted(bool expected)
    {
        var grantStoreMock = _fixture.Freeze<Mock<IGrantStore>>();
        grantStoreMock.Setup(x => x.Any(It.IsAny<Grant>()))
            .ReturnsAsync(expected);
        var grant = _fixture.Create<Grant>();
        var sut = _fixture.Create<ClientManager>();

        var actual = await sut.IsClientGrantedAsync(grant);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ValidateCodeAsync_Always_ShouldCheckIfGrantWithProvidedAuthCodeExists(bool expected)
    {
        var grantStoreMock = _fixture.Freeze<Mock<IGrantStore>>();
        grantStoreMock.Setup(x => x.AnyWithClientIdAndAuthCode(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(expected);
        var grant = _fixture.Create<Grant>();
        var sut = _fixture.Create<ClientManager>();

        var actual = await sut.IsClientGrantedAsync(grant);

        Assert.Equal(expected, actual);
    }

    public async Task ValidateCodeAsync_WhenSuccessful_ShouldSetAuthCodeAsUsed()
    {
        var grantStoreMock = _fixture.Freeze<Mock<IGrantStore>>();
    }

    [Fact]
    public async Task ValidateCodeAsync_WhenCodeWasAlreadyUsed_ShouldReturnFalseAndRevokeAllTokens()
    {
        
    }

    [Fact]
    public async Task ValidateCodeAsync_WhenCodeExpired_ShouldReturnFalse()
    {
        
    }
}
