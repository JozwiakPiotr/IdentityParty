using IdentityParty.Core.Abstractions;
using IdentityParty.Core.DTO;
using Microsoft.AspNetCore.Http;

namespace IdentityParty.Core.Endpoints.V1.Token;

internal sealed class TokenEndpoint : IEndpoint<TokenRequest>
{
    public string HttpVerb { get; }
    public string Path { get; }

    public Task<IResult> HandleAsync(TokenRequest Request)
    {
        throw new NotImplementedException();
    }
}