using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.ClientCred;

internal sealed class ClientCredentialsFlowHandler : IResponseTypeHandler
{
    public string GrantType { get; }

    public Task<AuthorizationResponse> HandleAsync(
        AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
}