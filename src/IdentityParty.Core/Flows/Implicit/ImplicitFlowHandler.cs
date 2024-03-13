using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.Implicit;

internal sealed class ImplicitFlowHandler : IResponseTypeHandler
{
    public string ResponseType { get; }

    public Task<AuthorizationResponse> HandleAsync(AuthorizationRequest request)
    {
        throw new NotImplementedException();
    }
}