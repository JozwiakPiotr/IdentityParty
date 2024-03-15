namespace IdentityParty.Core.DTO;

internal readonly struct TokenResponse
{
    public readonly SuccessfulTokenResponse? Success;
    public readonly ErrorTokenResponse? Error;

    private TokenResponse(SuccessfulTokenResponse success)
    {
        Success = success;
        Error = null;
    }

    private TokenResponse(ErrorTokenResponse error)
    {
        Error = error;
        Success = null;
    }

    public static implicit operator TokenResponse(SuccessfulTokenResponse success)
    {
        return new TokenResponse(success);
    }

    public static implicit operator TokenResponse(ErrorTokenResponse error)
    {
        return new TokenResponse(error);
    }
}