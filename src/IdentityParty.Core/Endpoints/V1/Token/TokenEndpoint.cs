using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Endpoints.V1.Token.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IdentityParty.Core.Endpoints.V1.Token;

internal sealed class TokenEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder builder)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> HandleAsync(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}