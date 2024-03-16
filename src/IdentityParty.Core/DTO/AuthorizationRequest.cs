using IdentityParty.Core.Entities;

namespace IdentityParty.Core.DTO;

internal record AuthorizationRequest
(
    List<Scope> Scope,
    string ResponseType,
    Guid ClientId,
    string RedirectUrl,
    string State
)
{
    // public string? State { get; set; }
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