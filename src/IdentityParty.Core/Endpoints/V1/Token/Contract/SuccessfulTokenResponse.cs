namespace IdentityParty.Core.Endpoints.V1.Token.Contract;

internal record SuccessfulTokenResponse
(
    string access_token,
    string token_type,
    int expires_in,
    string? refresh_token,
    /*OPTIONAL, if identical to the scope requested by the client;
         otherwise, REQUIRED.  The scope of the access token as
         described by Section 3.3.*/
    string? scope
);
