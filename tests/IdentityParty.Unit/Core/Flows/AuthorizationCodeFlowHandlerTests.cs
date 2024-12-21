using AutoFixture;
using AutoFixture.Xunit2;
using IdentityParty.Core;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Flows.AuthCode;
using Moq;

namespace IdentityParty.Unit.Core.Flows;

public class AuthorizationCodeFlowHandlerTests
{
    private readonly Fixture _fixture = FixtureFactory.Create();

    [Theory, AutoData]
    internal async Task HandleAsyncAuthorizationRequest_ShouldReturnSuccessfulResponse(
        [Frozen] Mock<IAuthCodeManager> authCodeManagerMock,
        AuthorizationCodeFlowHandler sut)
    {
        var request = _fixture.Create<AuthorizationRequest>();
        authCodeManagerMock.Setup(x => x.AssignAuthCodeAsync(request.Grant))
            .ReturnsAsync(_fixture.Create<string>());

        var actual = await sut.HandleAsync(request);

        Assert.NotNull(actual.Success);
        Assert.NotNull(actual.Success.Code);
        Assert.Equal(request.RedirectUrl, actual.Success.ReturnUrl);
        Assert.Equal(request.State, actual.Success.State);
    }
}
