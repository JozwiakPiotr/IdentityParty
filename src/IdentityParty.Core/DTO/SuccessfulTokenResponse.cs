namespace IdentityParty.Core.DTO;

internal record SuccessfulTokenResponse
(string AccessToken,
    string TokenType,
    int ExpiresIn,
    string? RefreshToken,
    string? Scope);