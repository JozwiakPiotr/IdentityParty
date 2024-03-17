using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Abstractions.Validators;

internal interface IResponseTypeValidator<THandler>
    where THandler : IResponseTypeHandler
{
    //TODO:add async suffix
    ValueTask<ErrorAuthorizationResponse> Validate(AuthorizationRequest request);
}