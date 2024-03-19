using IdentityParty.Core.Entities;

namespace IdentityParty.Core.DTO;

internal record AuthorizationRequest
(
    string ResponseType,
    string RedirectUrl,
    string? State,
    Grant Grant
)
{
    // public string? ResponseMode { get; set; }
    // public string? Nonce { get; set; }
    // public string? Display { get; set; }
    // public string? Prompt { get; set; }
    // public string? MaxAge { get; set; }
    // public string? UiLocales { get; set; }
    // public string? IdTokenHint { get; set; }
    // public string? LoginHint { get; set; }
    // public string? ArcValues { get; set; }
}