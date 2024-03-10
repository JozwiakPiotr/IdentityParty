using IdentityParty.Core.Abstractions.Validators;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.ClientCred;

internal sealed class ClientCredFlowValidator :
    IGrantTypeValidator<ClientCredentialsFlowHandler>
{
    public ValueTask<ErrorTokenResponse> Validate(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}