using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.Implicit;

internal sealed class ImplicitFlowHandler : IGrantTypeHandler
{
    public string ResponseType { get; }

    public Task<Either<SuccessfulTokenResponse, ErrorTokenResponse>> HandleAsync(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}