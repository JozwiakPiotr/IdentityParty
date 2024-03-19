using AutoFixture;
using IdentityParty.Core.Abstractions;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Flows.AuthCode;
using Moq;

namespace IdentityParty.Unit;

public class AuthorizationCodeFlowHandlerTests
{
    private AuthorizationCodeFlowHandler _sut;
    private Fixture _fixture;
    private Mock<IClientManager> _clientManager;


    public AuthorizationCodeFlowHandlerTests()
    {
        _fixture = FixtureFactory.GetFixture();
        _clientManager = _fixture.Freeze<Mock<IClientManager>>();
    }

    [Fact]
    public async Task HandleAsyncAuthorizationRequest_ShouldReturnSuccessfulResponse()
    {
        var request = _fixture.Create<AuthorizationRequest>();
        _clientManager.Setup(x => x.GetAuthCodeAsync(request.Grant))
            .ReturnsAsync(_fixture.Create<string>());
        _sut = _fixture.Create<AuthorizationCodeFlowHandler>();

        var actual = await _sut.HandleAsync(request);

        Assert.NotNull(actual.Success);
        Assert.NotNull(actual.Success.Code);
        Assert.Equal(request.RedirectUrl, actual.Success.ReturnUrl);
        Assert.Equal(request.State, actual.Success.State);
    }
}
