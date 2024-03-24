using AutoFixture;
using IdentityParty.Core;
using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Entities;
using Moq;

namespace IdentityParty.Unit;

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
        
    }

    [Fact]
    public async Task IsClientGrantedAsync_Always_ShouldReturnWetherClientIsGranted()
    {

    }

    [Fact]
    public async Task ValidateCodeAsync_WhenClientDoesntHaveAuthorizationCode_ShouldReturnFalse()
    {

    }

    [Fact]
    public async Task ValidateCodeAsync_WhenClientHaveAuthorizationCode_ShouldCompareCodes()
    {

    }
}
