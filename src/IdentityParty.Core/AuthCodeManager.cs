using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Entities;

namespace IdentityParty.Core;

public class AuthCodeManager : IAuthCodeManager
{
    public AuthCodeManager(
        IGrantStore grantStore,
        IAuthorizationCodeIssuer authCodeIssuer
    )
    {

    }

    public Task<string> AssignAuthCodeAsync(Grant grant)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ValidateAuthorizationCodeAsync(string passedCode, Guid clientId)
    {
        throw new NotImplementedException();
    }
}
