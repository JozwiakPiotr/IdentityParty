using IdentityParty.Core.Abstractions.Validators;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.Implicit;

internal sealed class ImplicitFlowValidator : 
    IResponseTypeValidator<ImplicitFlowHandler>
{
    public ValueTask<ErrorAuthorizationResponse> Validate(AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
}