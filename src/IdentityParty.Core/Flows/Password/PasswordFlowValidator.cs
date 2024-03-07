using IdentityParty.Core.Abstractions.Validators;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Flows.Password;

internal class PasswordFlowValidator : IGrantTypeValidator<PasswordFlowHandler>
{
    public ValueTask<ErrorTokenResponse> Validate(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}