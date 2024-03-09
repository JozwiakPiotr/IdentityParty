using AutoFixture;
using AutoFixture.AutoMoq;
using IdentityParty.Core;
using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.Endpoints.V1.Authorization;
using IdentityParty.Core.Endpoints.V1.Authorization.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Moq;

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
        var context = _fixture.Create<HttpContext>();
        var request = _fixture.Create<AuthorizationRequest>();
        var options = _fixture.Create<IOptions<IdentityPartyOptions>>();
        var clientManager = _fixture.Create<IClientManager>();
        var handlerMock = new Mock<IResponseTypeHandler>();

        var result = await _sut.HandleAsync(context, request, options, clientManager, handlerMock.Object);

        Assert.IsType<Ok<SuccessfulAuthorizationResponse>>(result);
        var okResult = (Ok<SuccessfulAuthorizationResponse>)result;
        Assert.NotNull(okResult.Value);
    }
}