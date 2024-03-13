using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Abstractions.Handlers;

internal interface IGrantTypeHandler
{
    string GrantType { get; }

    Task<Either<SuccessfulTokenResponse, ErrorTokenResponse>>
        HandleAsync(TokenRequest request);
}