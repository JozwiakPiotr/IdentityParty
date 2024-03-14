using IdentityParty.Core.DTO;

namespace IdentityParty.Core;

internal interface IAuthorizationFlowResolver
{
    Task<AuthorizationResponse> HandleAsync(AuthorizationRequest request);
}