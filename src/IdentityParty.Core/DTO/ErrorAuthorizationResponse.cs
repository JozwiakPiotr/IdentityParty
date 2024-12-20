namespace IdentityParty.Core.DTO;

internal record ErrorAuthorizationResponse(
    string Error,
    string? State
    );