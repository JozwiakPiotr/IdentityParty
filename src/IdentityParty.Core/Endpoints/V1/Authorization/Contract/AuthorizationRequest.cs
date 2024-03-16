using System.Text.Json.Serialization;

namespace IdentityParty.Core.Endpoints.V1.Authorization.Contract;

internal record AuthorizationRequest(
    [property: JsonPropertyName("scope")] string Scope,
    [property: JsonPropertyName("response_type")]
    string ResponseType,
    [property: JsonPropertyName("client_id")]
    string ClientId,
    [property: JsonPropertyName("redirect_uri")]
    string RedirectUri,
    [property: JsonPropertyName("state")]
    string state);
/*
 * TODO: add optional parameteres
 * state
 * reponse_mode
 * nonce
 * display
 * prompt
 * max_age
 * ui_locales
 * id_token_hint
 * login_hint
 * arc_values
 */