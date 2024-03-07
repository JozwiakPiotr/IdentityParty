using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Flows.ClientCred;

internal sealed class ClientCredentialsFlowHandler : IGrantTypeHandler
{
    public string GrantType { get; }

    public Task<Either<SuccessfulAuthorizationResponse, ErrorAuthorizationResponse>> HandleAsync(
        AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
}