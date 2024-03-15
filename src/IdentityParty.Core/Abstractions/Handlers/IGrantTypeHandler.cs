using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Abstractions.Handlers;

internal interface IGrantTypeHandler
{
    string GrantType { get; }

    Task<TokenResponse> HandleAsync(TokenRequest request);
}