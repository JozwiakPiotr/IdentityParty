using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.Password;

internal class PasswordFlowHandler : IResponseTypeHandler
{
    public string GrantType { get; }

    public Task<Either<SuccessfulAuthorizationResponse, ErrorAuthorizationResponse>> HandleAsync(
        AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
}