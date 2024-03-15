namespace IdentityParty.Core.DTO;

internal readonly struct AuthorizationResponse
{
    public readonly SuccessfulAuthorizationResponse? Success;
    public readonly ErrorAuthorizationResponse? Error;

    public AuthorizationResponse(SuccessfulAuthorizationResponse success)
    {
        Success = success;
        Error = null;
    }

    public AuthorizationResponse(ErrorAuthorizationResponse error)
    {
        Error = error;
        Success = null;
    }

    public static implicit operator AuthorizationResponse(SuccessfulAuthorizationResponse success)
    {
        return new AuthorizationResponse(success);
    }

    public static implicit operator AuthorizationResponse(ErrorAuthorizationResponse error)
    {
        return new AuthorizationResponse(error);
    }
}