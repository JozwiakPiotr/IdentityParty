using System.Reflection;
using System.Security.Claims;
using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Endpoints.V1.Authorization.Contract;
using IdentityParty.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using AuthorizationRequest = IdentityParty.Core.Endpoints.V1.Authorization.Contract.AuthorizationRequest;
using SuccessfulAuthorizationResponse = IdentityParty.Core.Endpoints.V1.Authorization.Contract.SuccessfulAuthorizationResponse;

namespace IdentityParty.Core.Endpoints.V1.Authorization;

internal sealed class AuthorizationEndpoint : IEndpoint<AuthorizationRequest>
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

    public string HttpVerb { get; } = HttpMethods.Get;
    public string Path { get; } = "authorization";

    public async Task<IResult> HandleAsync(AuthorizationRequest request)
    {
        if (await ClientDoesntExist(request.ClientId))
            return Results.Unauthorized();

        if (UserIsNotAuthenticated())
            RedirectToLoginPageResult(request);

        if (await ClientIsNotGranted(request))
            RedirectToConsentPageResult(request);

        var result = await _responseTypeHandler.HandleAsync(ToCoreRequest(request));
        return result.Match(
            success => Results.Ok(ToSuccess(success)),
            error => Results.BadRequest(ToError(error)));
    }

    private Task<bool> ClientDoesntExist(string clientId)
    {
        _ = Guid.TryParse(clientId, out var parsed);
        return _clientManager.DoesClientExistAsync(parsed);
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

    private static DTO.AuthorizationRequest ToCoreRequest(AuthorizationRequest request)
    {
        return new DTO.AuthorizationRequest(
            request.Scope.Split(' ').Select(x => new Scope(x)).ToList(),
            request.ResponseType,
            Guid.Parse(request.ClientId),
            request.RedirectUri
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