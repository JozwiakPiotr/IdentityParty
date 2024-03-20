namespace IdentityParty.Core;

internal record ErrorTokenResponse
(
    string error,
    string? error_description,
    string? error_uri
);
