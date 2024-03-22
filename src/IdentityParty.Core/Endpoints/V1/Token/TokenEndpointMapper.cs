using IdentityParty.Core.DTO;
using SuccessfulTokenResponse = IdentityParty.Core.Endpoints.V1.Token.Contract.SuccessfulTokenResponse;

namespace IdentityParty.Core.Endpoints.V1.Token;

internal static class TokenEndpointMapper
{
    internal static TokenRequest ToCoreRequest(this Contract.TokenRequest request)
    {
        return new TokenRequest(
            request.grant_type,
            request.code,
            request.redirect_uri,
            request.client_id);
    }

    internal static SuccessfulTokenResponse ToSuccessfulResponse(this DTO.SuccessfulTokenResponse response)
    {
        return new SuccessfulTokenResponse(
            response.AccessToken,
            response.TokenType,
            response.ExpiresIn,
            response.RefreshToken,
            response.Scope);
    }

    internal static ErrorTokenResponse ToErrorResponse(this DTO.ErrorTokenResponse response)
    {
        return new ErrorTokenResponse(
            response.Error,
            response.ErrorDescription,
            response.ErrorUri);
    }
}