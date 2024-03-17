using AutoFixture;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Flows.AuthCode;

namespace IdentityParty.Unit;

public class AuthorizationCodeFlowHandlerTests
{
    private AuthorizationCodeFlowHandler _sut;
    private Fixture _fixture;

    public AuthorizationCodeFlowHandlerTests()
    {
        _fixture = FixtureFactory.GetFixture();
        _sut = new();
    }

    [Fact]
    public async Task HandleAsyncAuthorizationRequest_WhenRequestIsValid_ShouldReturnValidResponse()
    {
        var request = _fixture.Create<AuthorizationRequest>();

        var actual = await _sut.HandleAsync(request);

        Assert.NotNull(actual.Success);
        Assert.NotNull(actual.Success.Code);
        Assert.Equal(request.RedirectUrl, actual.Success.ReturnUrl);
        Assert.Equal(request.State, actual.Success.State);
    }
}
