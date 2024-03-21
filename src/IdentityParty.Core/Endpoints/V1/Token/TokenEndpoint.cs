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
        var h = _handlers.FirstOrDefault(x => x.GrantType == request.grant_type);
        if(h is null)
            return UnsuportedGrantTypeResponse();
        var coreRequest = ToCoreRequest(request);
        var result = await h.HandleAsync(coreRequest);
        return MatchResult(result);
    }
        

    private IResult UnsuportedGrantTypeResponse()
    {
        throw new NotImplementedException();
    }

    private IResult MatchResult(DTO.TokenResponse response) =>
        response switch
        {
            (null, var err) => throw new NotImplementedException(),
            (var success, null) => throw new NotImplementedException()
        };

    private DTO.TokenRequest ToCoreRequest(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}