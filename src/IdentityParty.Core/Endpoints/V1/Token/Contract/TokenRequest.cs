namespace IdentityParty.Core.Endpoints.V1.Token.Contract;

internal record TokenRequest
(
    string grant_type,
    string code,
    string redirect_uri,
    string client_id
);