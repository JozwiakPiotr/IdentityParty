using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Abstractions.Validators;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.AuthCode;

internal sealed class AuthCodeFlowValidator :
    IResponseTypeValidator<AuthorizationCodeFlowHandler>,
    IGrantTypeValidator<AuthorizationCodeFlowHandler>
{
    private readonly IScopeStore _scopes;
    AuthCodeFlowValidator(IScopeStore scopes)
    {
        _scopes = scopes;
    }

    public ValueTask<ErrorTokenResponse> Validate(TokenRequest request)
    {
        throw new NotImplementedException();
    }

    public ValueTask<ErrorAuthorizationResponse> Validate(AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
}