﻿using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;

namespace IdentityParty.Core.Flows.ClientCred;

internal sealed class ClientCredentialsFlowHandler : IGrantTypeHandler
{
    public string GrantType => throw new NotImplementedException();

    public Task<TokenResponse> HandleAsync(TokenRequest request)
    {
        throw new NotImplementedException();
    }
}