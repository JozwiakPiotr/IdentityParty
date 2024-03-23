using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.Endpoints.V1.Token.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IdentityParty.Core.Endpoints.V1.Token;

internal sealed class TokenEndpoint : IEndpoint
{
    private IEnumerable<IGrantTypeHandler> _handlers;

    public TokenEndpoint(IEnumerable<IGrantTypeHandler> handlers)
    {
        _handlers = handlers;
    }

    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("token",
            (TokenRequest request, TokenEndpoint endpoint) =>
                endpoint.HandleAsync(request));
    }

    public async Task<IResult> HandleAsync(TokenRequest request)
    {
        var handler = _handlers.FirstOrDefault(x => x.GrantType == request.grant_type); 
        if (handler is null)
            return UnsupportedGrantTypeResponse();
        var coreRequest = request.ToCoreRequest();
        var result = await handler.HandleAsync(coreRequest);
        if (result.Success is null)
            return MatchError(result.Error!);
        return Results.Ok(result.Success.ToSuccessfulResponse());
    }

    private IResult UnsupportedGrantTypeResponse()
    {
        //TODO: add description and error page uri
        var error = new ErrorTokenResponse("unsupported_grant_type", null, null);
        return Results.BadRequest(error);
    }

    private IResult MatchError(DTO.ErrorTokenResponse error)
    {
        return error.Error switch
        {
            "invalid_request" or
                "unauthorized_client" or
                "invalid_grant" or
                "invalid_scope" => Results.BadRequest(error.ToErrorResponse()),
            //TODO: Add body
            //TODO: Decide where the authentication and authorization should be
            "invalid_client" => TypedResults.Unauthorized(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}