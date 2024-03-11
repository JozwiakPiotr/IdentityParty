namespace IdentityParty.Core.DTO;

internal sealed class AuthorizationResponse
    : Either<SuccessfulAuthorizationResponse, ErrorAuthorizationResponse>
{
    private AuthorizationResponse(SuccessfulAuthorizationResponse left)
        : base(left)
    {
    }

    private AuthorizationResponse(ErrorAuthorizationResponse right)
        : base(right)
    {
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