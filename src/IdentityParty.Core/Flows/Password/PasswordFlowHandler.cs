using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.Password;

internal class PasswordFlowHandler : IResponseTypeHandler
{
    public string ResponseType => throw new NotImplementedException();

    public Task<AuthorizationResponse> HandleAsync(AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
}