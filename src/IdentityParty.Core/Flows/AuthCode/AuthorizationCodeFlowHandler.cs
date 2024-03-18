using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.AuthCode;

internal sealed class AuthorizationCodeFlowHandler :
    IGrantTypeHandler, IResponseTypeHandler
{
    private readonly IClientManager _clientManager;
    public AuthorizationCodeFlowHandler(IClientManager clientManager)
    {
        _clientManager = clientManager;
    }
    public Task<AuthorizationResponse> HandleAsync(AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
    public string ResponseType { get; } = "code";

    public Task<TokenResponse> HandleAsync(
        TokenRequest request)
    {
        throw new NotImplementedException();
    }

    public string GrantType { get; }
}