using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows;

internal sealed class AuthorizationFlowResolver : IAuthorizationFlowResolver
{
    public Task<AuthorizationResponse> HandleAsync(AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
}