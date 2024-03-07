using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;

namespace IdentityParty.Core.Flows.Implicit;

internal sealed class ImplicitFlowHandler : IResponseTypeHandler
{
    public string ResponseType { get; }

    public Task<Either<SuccessfulTokenResponse, ErrorTokenResponse>> HandleAsync(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}