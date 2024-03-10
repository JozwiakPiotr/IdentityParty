using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Abstractions.Handlers;

internal interface IResponseTypeHandler
{
    string GrantType { get; }

    Task<Either<SuccessfulAuthorizationResponse, ErrorAuthorizationResponse>>
        HandleAsync(AuthorizationRequest request);
}