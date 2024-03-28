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
}
