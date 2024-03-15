using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.Password;

internal class PasswordFlowHandler : IGrantTypeHandler
{
    public string GrantType { get; }

    public Task<TokenResponse> HandleAsync(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}