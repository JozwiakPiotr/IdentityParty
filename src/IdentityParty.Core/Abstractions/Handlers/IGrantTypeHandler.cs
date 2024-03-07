using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Abstractions.Handlers;

internal interface IGrantTypeHandler
{
    string GrantType { get; }
    
    Task<Either<SuccessfulAuthorizationResponse, ErrorAuthorizationResponse>>
        HandleAsync(AuthorizationRequest request);
}