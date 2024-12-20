namespace IdentityParty.Core.DTO;

internal record ErrorTokenResponse(
    string Error,
    string? ErrorDescription,
    string? ErrorUri
    );