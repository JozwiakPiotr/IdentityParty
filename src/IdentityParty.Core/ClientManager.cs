﻿using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Entities;

namespace IdentityParty.Core;

internal class ClientManager : IClientManager
{
    public ClientManager(
        IGrantStore grantStore,
        IClientStore clientStore)
    {

    }

    public Task<bool> DoesClientExistAsync(Guid clientId)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAuthCodeAsync(Grant grant)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsClientGrantedAsync(Grant grant)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ValidateAuthorizationCodeAsync(string code, Guid clientId)
    {
        throw new NotImplementedException();
    }
}
