using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Abstractions.Handlers;

internal interface IGrantTypeHandler
{
    string ResponseType { get; }
    
    Task<Either<SuccessfulTokenResponse, ErrorTokenResponse>>
        HandleAsync(TokenRequest request);
}