using IdentityParty.Core.Abstractions.Validators;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Flows.ClientCred;

internal sealed class ClientCredFlowValidator : 
    IGrantTypeValidator<ClientCredentialsFlowHandler>
{
    public ValueTask<ErrorTokenResponse> Validate(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}