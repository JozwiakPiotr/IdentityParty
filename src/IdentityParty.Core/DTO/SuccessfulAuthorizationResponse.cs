namespace IdentityParty.Core.DTO;

internal record SuccessfulAuthorizationResponse
(
    string ReturnUrl,
    string Code,
    string? State
);