using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Abstractions.Validators;

internal interface IResponseTypeValidator<THandler>
    where THandler : IGrantTypeHandler
{
    ValueTask<ErrorAuthorizationResponse> Validate(AuthorizationRequest request);
}