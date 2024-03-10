using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.AuthCode;

internal sealed class AuthorizationCodeFlowHandler :
    IGrantTypeHandler, IResponseTypeHandler
{
    public string ResponseType { get; }

    public Task<Either<SuccessfulTokenResponse, ErrorTokenResponse>> HandleAsync(
        TokenRequest request)
    {
        throw new NotImplementedException();
    }

    public string GrantType { get; }

    public Task<Either<SuccessfulAuthorizationResponse, ErrorAuthorizationResponse>> HandleAsync(
        AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
}