using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Abstractions.Handlers;

internal interface IResponseTypeHandler
{
    Task<Either<SuccessfulTokenResponse, ErrorTokenResponse>>
        HandleAsync(TokenRequest request);
}