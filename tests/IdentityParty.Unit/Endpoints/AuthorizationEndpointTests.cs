using AutoFixture;
using AutoFixture.AutoMoq;
using IdentityParty.Core.Endpoints.V1.Authorization;
using IdentityParty.Core.Endpoints.V1.Authorization.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using AuthorizationRequest = IdentityParty.Core.DTO.AuthorizationRequest;

namespace IdentityParty.Unit.Endpoints;

public class AuthorizationEndpointTests
{
    private AuthorizationEndpoint _sut;
    private Fixture _fixture;
    
    public AuthorizationEndpointTests()
    {
        _sut = new AuthorizationEndpoint();
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());
    }
    
    [Fact]
    public void Handle_WhenUserIsNotAuthenticated_ShouldReturn302ToLoginPage()
    {
        
    }

    [Fact]
    public void Handle_WhenClientIsNotGranted_ShouldReturn302ToConsentPage()
    {
        
    }

    [Fact]
    public async Task Handle_WhenUserIsAuthenticatedAndClientIsGranted_ShouldReturn200WithSuccessfulResponse()
    {
        var request = _fixture.Create<AuthorizationRequest>();
        var context = _fixture.Create<HttpContext>();

        var result = await _sut.Handle(context, request);

        Assert.IsType<Ok<SuccessfulAuthorizationResponse>>(result);
        var okResult = (Ok<SuccessfulAuthorizationResponse>)result;
        Assert.NotNull(okResult.Value);
    }
}