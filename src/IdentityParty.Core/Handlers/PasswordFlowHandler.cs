using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Handlers;

internal class PasswordFlowHandler : IGrantTypeHandler
{
    public Task<Either<SuccessfulAuthorizationResponse, ErrorAuthorizationResponse>> HandleAsync(
        AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
}