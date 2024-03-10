using System.Security.Claims;
using AutoFixture;
using AutoFixture.AutoMoq;
using IdentityParty.Core;
using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Endpoints.V1.Authorization;
using IdentityParty.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Moq;
using AuthorizationRequest = IdentityParty.Core.Endpoints.V1.Authorization.Contract.AuthorizationRequest;
using Claim = System.Security.Claims.Claim;
using SuccessfulAuthorizationResponse = IdentityParty.Core.Endpoints.V1.Authorization.Contract.SuccessfulAuthorizationResponse;

namespace IdentityParty.Unit.Endpoints;

public class AuthorizationEndpointTests
{
    private readonly Mock<IClientManager> _clientManagerMock = new();
    private readonly Fixture _fixture;
    private readonly Mock<IResponseTypeHandler> _handlerMock = new();
    private readonly Mock<IOptions<IdentityPartyOptions>> _optionsMock = new();
    private readonly AuthorizationEndpoint _sut = new();

    public AuthorizationEndpointTests()
    {
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
        //Arrange
        Either<IdentityParty.Core.DTO.SuccessfulAuthorizationResponse, ErrorAuthorizationResponse> 
            response = _fixture.Create<IdentityParty.Core.DTO.SuccessfulAuthorizationResponse>();
        const bool userAuthenticated = true;
        var clientId = Guid.NewGuid();
        var context = GetHttpContextMock(userAuthenticated);
        var request = _fixture.Build<AuthorizationRequest>()
            .With(x => x.ClientId, clientId.ToString).Create();
        _clientManagerMock.Setup(x => x.DoesClientExistAsync(clientId))
            .ReturnsAsync(true);
        _clientManagerMock.Setup(x => x.IsClientGrantedAsync(It.IsAny<Grant>()))
            .ReturnsAsync(true);
        _handlerMock.Setup(x => x.HandleAsync(It.IsAny<IdentityParty.Core.DTO.AuthorizationRequest>()))
            .ReturnsAsync(response);

        //Act
        var result = await _sut.HandleAsync(context, request,
            _optionsMock.Object,
            _clientManagerMock.Object,
            _handlerMock.Object);

        //Assert
        Assert.IsType<Ok<SuccessfulAuthorizationResponse>>(result);
        var okResult = (Ok<SuccessfulAuthorizationResponse>)result;
        Assert.NotNull(okResult.Value);
    }

    private static HttpContext GetHttpContextMock(bool isUserAuthenticated)
    {
        // var claims = new List<Claim>();
        // var idMock = new Mock<ClaimsIdentity>();
        // if (isUserAuthenticated)
        // {
        //     claims.Add(
        //         new Claim(ClaimTypes.NameIdentifier,
        //             Guid.NewGuid().ToString())
        //     );
        //     idMock.SetupGet(x => x.IsAuthenticated).Returns(true);
        // }
        //
        // idMock.SetupGet(x => x.Claims).Returns(claims);
        // var httpMock = new Mock<HttpContext>();
        // httpMock.SetupGet(x => x.User.Identity.IsAuthenticated)
        //     .Returns(true);
        // return new DefaultHttpContext
        // {
        //     User = new ClaimsPrincipal(idMock.Object)
        // };

        var mock = new Mock<HttpContext>();
        var idMock = new Mock<ClaimsIdentity>();
        idMock.SetupGet(x => x.IsAuthenticated).Returns(true);
        mock.Setup(x => x.User.FindFirst(ClaimTypes.NameIdentifier))
            .Returns(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
        mock.SetupGet(x => x.User.Identity).Returns(idMock.Object);
        return mock.Object;
    }
}