using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Flows.AuthCode;

internal sealed class AuthorizationCodeFlowHandler : 
    IResponseTypeHandler, IGrantTypeHandler
{
    public string GrantType { get; }

    public Task<Either<SuccessfulAuthorizationResponse, ErrorAuthorizationResponse>> HandleAsync(
        AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }

    public string ResponseType { get; }

    public Task<Either<SuccessfulTokenResponse, ErrorTokenResponse>> HandleAsync(
        TokenRequest request)
    {
        throw new NotImplementedException();
    }
}