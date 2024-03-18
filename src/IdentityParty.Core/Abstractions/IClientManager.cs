﻿using IdentityParty.Core.Entities;

namespace IdentityParty.Core.Abstractions;

public interface IClientManager
{
    Task<bool> IsClientGrantedAsync(Grant grant);
    Task<bool> DoesClientExistAsync(Guid clientId);
    Task<string> GetAuthCodeAsync(Guid clientId);
    Task<bool> ValidateCodeAsync(string code);
}