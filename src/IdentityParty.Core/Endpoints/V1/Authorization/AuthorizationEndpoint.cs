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
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("authorization", HandleAsync);
    }

    public async Task<IResult> HandleAsync(
        HttpContext ctx, AuthorizationRequest request,
        IOptions<IdentityPartyOptions> opt,
        IClientManager clientManager,
        IResponseTypeHandler handler)
    {
        var options = opt.Value;

        if (!await clientManager.DoesClientExistAsync(Guid.Parse(request.ClientId)))
            return Results.Unauthorized();
        if (!ctx.User.Identity?.IsAuthenticated ?? true)
            return Results.LocalRedirect(
                options.LoginPageRelativeUrl +
                ToQueryString(request));

        var userId = GetUserId(ctx);
        if (!await clientManager.IsClientGrantedAsync(
                ToGrant(request, userId)))
            return Results.LocalRedirect(
                options.ConsentPageRelativeUrl +
                ToQueryString(request));

        var result = await handler.HandleAsync(ToCoreRequest(request));
        return result.Match(
            success => Results.Ok(ToSuccess(success)),
            error => Results.BadRequest(ToError(error)));
    }

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
}