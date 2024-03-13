using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Abstractions.Handlers;

internal interface IResponseTypeHandler
{
    string ResponseType { get; }

    Task<AuthorizationResponse>
        HandleAsync(AuthorizationRequest request);
}