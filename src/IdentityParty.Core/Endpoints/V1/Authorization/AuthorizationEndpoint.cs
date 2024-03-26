using System.Reflection;
using System.Security.Claims;
using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Endpoints.V1.Authorization.Contract;
using IdentityParty.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using AuthorizationRequest = IdentityParty.Core.Endpoints.V1.Authorization.Contract.AuthorizationRequest;
using SuccessfulAuthorizationResponse = IdentityParty.Core.Endpoints.V1.Authorization.Contract.SuccessfulAuthorizationResponse;

namespace IdentityParty.Core.Endpoints.V1.Authorization;

internal sealed class AuthorizationEndpoint : IEndpoint
{
    private readonly HttpContext _httpContext;
    private readonly IdentityPartyOptions _options;
    private readonly IClientManager _clientManager;
    private readonly IResponseTypeHandler _responseTypeHandler;

    public AuthorizationEndpoint(
        IHttpContextAccessor accessor,
        IOptions<IdentityPartyOptions> options,
        IClientManager clientManager,
        IResponseTypeHandler responseTypeHandler)
    {
        _options = options.Value;
        //TODO: is it possible to have null reference here?
        _httpContext = accessor.HttpContext!;
        _clientManager = clientManager;
        _responseTypeHandler = responseTypeHandler;
    }

    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("authorization",
            async (AuthorizationRequest req, AuthorizationEndpoint end) =>
                await end.HandleAsync(req))
            .AllowAnonymous();
    }

    //TODO: validate returnUrl
    /*
    request |> validate |> match result with
        err -> match err.code with 
            invalid_request -> 302(redirect_uri)
            invalid_redirect_uri -> 302(error_page)
            ...
        success ->
            result |> ToCore |> handler.Handle |> match result with
            ... 
    */
    public async Task<IResult> HandleAsync(AuthorizationRequest request)
    {
        if (await ClientDoesntExist(request.ClientId))
            return Results.Unauthorized();

        if (UserIsNotAuthenticated())
            RedirectToLoginPageResult(request);

        if (await ClientIsNotGranted(request))
            RedirectToConsentPageResult(request);

        var result = await _responseTypeHandler.HandleAsync(ToCoreRequest(request));

        return result.Success is not null
            ? Results.Ok(ToSuccess(result.Success))
            : Results.BadRequest(ToError(result.Error!));
    }

    private async Task<bool> ClientDoesntExist(string clientId)
    {
        _ = Guid.TryParse(clientId, out var parsed);
        return !await _clientManager.DoesClientExistAsync(parsed);
    }

    private bool UserIsNotAuthenticated()
    {
        return !_httpContext.User.Identity?.IsAuthenticated ?? true;
    }

    private IResult RedirectToLoginPageResult(AuthorizationRequest request)
    {
        return Results.LocalRedirect(
            _options.LoginPageRelativeUrl +
            ToQueryString(request));
    }

    private async Task<bool> ClientIsNotGranted(AuthorizationRequest request)
    {
        var userId = GetUserId(_httpContext);
        return !await _clientManager.IsClientGrantedAsync(
            ToGrant(request, userId));
    }

    private IResult RedirectToConsentPageResult(AuthorizationRequest request)
    {
        return Results.LocalRedirect(
            _options.ConsentPageRelativeUrl +
            ToQueryString(request));
    }

    //TODO:move to better place

    #region MyRegion

    private static string ToQueryString(AuthorizationRequest request)
    {
        var queries = request.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance)
            .Select(x => $"{x.Name}={x.GetValue(request)}");
        return "?" + string.Join('&', queries);
    }

    private static Grant ToGrant(AuthorizationRequest request,
        Guid userId)
    {
        return new Grant(
            Guid.Parse(request.ClientId),
            userId,
            request.Scope.Split(' ').Select(x => new Scope(x)).ToList()
        );
    }

    private static Guid GetUserId(HttpContext ctx)
    {
        return Guid.Parse(ctx.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }

    private DTO.AuthorizationRequest ToCoreRequest(AuthorizationRequest request)
    {
        return new DTO.AuthorizationRequest(
            request.ResponseType,
            request.RedirectUri,
            request.state,
            new Grant(
                Guid.Parse(request.ClientId),
                GetUserId(_httpContext),
                request.Scope.Split(' ').Select(x => new Scope(x)).ToList())
        );
    }

    private static SuccessfulAuthorizationResponse ToSuccess(
        DTO.SuccessfulAuthorizationResponse success)
    {
        return new SuccessfulAuthorizationResponse();
    }

    private static AuthorizationErrorResponse ToError(
        ErrorAuthorizationResponse error)
    {
        return new AuthorizationErrorResponse(error.Error, error.State);
    }

    #endregion
}