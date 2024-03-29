using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.AuthCode;

internal sealed class AuthorizationCodeFlowHandler :
    IGrantTypeHandler, IResponseTypeHandler
{
    private readonly IAuthCodeManager _authCodeManager;

    public AuthorizationCodeFlowHandler(IAuthCodeManager authCodeManager)
    {
        _authCodeManager = authCodeManager;
    }
    public async Task<AuthorizationResponse> HandleAsync(AuthorizationRequest request)
    {
        var code = await _authCodeManager.AssignAuthCodeAsync(request.Grant);
        return new SuccessfulAuthorizationResponse(
            request.RedirectUrl,
            code,
            request.State);
    }
    public string ResponseType { get; } = "code";

    public Task<TokenResponse> HandleAsync(
        TokenRequest request)
    {
        throw new NotImplementedException();
    }

    public string GrantType { get; } = "authorization_code";
}