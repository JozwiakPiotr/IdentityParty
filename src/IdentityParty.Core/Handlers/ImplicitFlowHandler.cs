using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Handlers;

internal class ImplicitFlowHandler : IResponseTypeHandler
{
    public Task<Either<SuccessfulTokenResponse, ErrorTokenResponse>> HandleAsync(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}