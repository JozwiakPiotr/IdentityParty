using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Handlers;

internal class AuthorizationCodeFlowHandler : IResponseTypeHandler, IGrantTypeHandler
{
    public Task<Either<SuccessfulAuthorizationResponse, ErrorAuthorizationResponse>> HandleAsync(
        AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Either<SuccessfulTokenResponse, ErrorTokenResponse>> HandleAsync(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}