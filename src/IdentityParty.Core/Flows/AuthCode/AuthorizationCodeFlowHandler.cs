using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.AuthCode;

internal sealed class AuthorizationCodeFlowHandler :
    IGrantTypeHandler, IResponseTypeHandler
{
    public Task<AuthorizationResponse> HandleAsync(AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
    public string ResponseType { get; }

    public Task<TokenResponse> HandleAsync(
        TokenRequest request)
    {
        throw new NotImplementedException();
    }

    public string GrantType { get; }
}