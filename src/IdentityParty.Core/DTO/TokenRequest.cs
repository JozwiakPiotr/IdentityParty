namespace IdentityParty.Core.DTO;

public record TokenRequest
(
    string GrantType,
    string Code,
    string RedirectUri,
    string ClientId
);