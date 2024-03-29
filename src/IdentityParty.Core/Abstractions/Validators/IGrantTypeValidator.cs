﻿using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Abstractions.Validators;

internal interface IGrantTypeValidator<THandler>
    where THandler : IGrantTypeHandler
{
    ValueTask<ErrorTokenResponse> Validate(TokenRequest request);
}