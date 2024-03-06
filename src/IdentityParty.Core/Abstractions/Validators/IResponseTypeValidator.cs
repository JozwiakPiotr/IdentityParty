using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Abstractions.Validators;

internal interface IResponseTypeValidator<THandler>
    where THandler : IResponseTypeHandler
{
    ValueTask<ErrorAuthorizationResponse> Validate(AuthorizationRequest request);
}