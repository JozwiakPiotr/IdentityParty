using IdentityParty.Core.Abstractions.Validators;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Flows.AuthCode;

internal sealed class AuthCodeFlowValidator : 
    IResponseTypeValidator<AuthorizationCodeFlowHandler>,
    IGrantTypeValidator<AuthorizationCodeFlowHandler>
{
    public ValueTask<ErrorAuthorizationResponse> Validate(AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }

    public ValueTask<ErrorTokenResponse> Validate(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}