using IdentityParty.Core.Entities;

namespace IdentityParty.Core;

public interface IAuthCodeManager
{
    Task<string> AssignAuthCodeAsync(Grant grant);
    Task<bool> ValidateAuthorizationCodeAsync(string passedCode, Guid clientId);
}
