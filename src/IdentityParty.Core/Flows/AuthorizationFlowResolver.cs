using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows;

internal sealed class AuthorizationFlowResolver : IAuthorizationFlowResolver
{
    private readonly IEnumerable<IResponseTypeHandler> _handlers;

    public AuthorizationFlowResolver(IEnumerable<IResponseTypeHandler> handlers)
    {
        _handlers = handlers;
    }

    public async Task<AuthorizationResponse> HandleAsync(AuthorizationRequest request)
    {
        var handler = _handlers.SingleOrDefault(x => x.ResponseType == request.ResponseType);
        if (handler is null)
            return new ErrorAuthorizationResponse(
                "unsupported_response_type", request.RedirectUrl);
        return await handler.HandleAsync(request);
    }
}