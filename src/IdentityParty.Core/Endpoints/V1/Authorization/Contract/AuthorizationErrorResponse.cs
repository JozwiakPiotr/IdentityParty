namespace IdentityParty.Core.Endpoints.V1.Authorization.Contract;

internal record AuthorizationErrorResponse(
    string error,
    string? state
);