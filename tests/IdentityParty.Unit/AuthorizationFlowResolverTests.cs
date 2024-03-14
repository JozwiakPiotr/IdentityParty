using AutoFixture;
using AutoFixture.AutoMoq;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Flows;

namespace IdentityParty.Unit;

public class AuthorizationFlowResolverTests
{
    private AuthorizationFlowResolver _sut = new();
    private Fixture _fixture;

    public AuthorizationFlowResolverTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());
    }

    [Fact]
    public async Task HandleAsync_WhenResponseTypeIsNotSupported_ShouldReturnErrorResponse()
    {
        var request = _fixture.Build<AuthorizationRequest>()
            .With(x => x.ResponseType, "")
            .Create();
        
        var response = await _sut.HandleAsync(request);

        //Assert.True(response.Match())
    }
}
