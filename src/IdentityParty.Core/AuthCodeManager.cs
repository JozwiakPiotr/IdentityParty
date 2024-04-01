using IdentityParty.Core.Abstractions;
using IdentityParty.Core.Entities;

namespace IdentityParty.Core;

public class AuthCodeManager : IAuthCodeManager
{
    private readonly IGrantStore _grantStore;
    private readonly IAuthorizationCodeIssuer _authCodeIssuer;
    private readonly IAccessTokenManager _accessTokenManager;
    private readonly IIdTokenManager _idTokenManager;
    public AuthCodeManager(
        IGrantStore grantStore,
        IAuthorizationCodeIssuer authCodeIssuer,
        IAccessTokenManager accessTokenManager,
        IIdTokenManager idTokenManager
    )
    {
        _grantStore = grantStore;
        _authCodeIssuer = authCodeIssuer;
        _accessTokenManager = accessTokenManager;
        _idTokenManager = idTokenManager;
    }

    public async Task<string> AssignAuthCodeAsync(Grant grant)
    {
        var code = _authCodeIssuer.Issue();
        grant.AssignAuthCode(code);
        await _grantStore.UpdateAsync(grant);
        return code;
    }

    public Task<bool> ValidateAuthorizationCodeAsync(string passedCode, Guid clientId)
    {
        var grant = await _grantStore.GetAsync(clientId, passedCode);
        if(grant is null)
            return false;
        if(passedCode != grant.AuthorizationCode)
            return false;
        if(grant.WasUsed)
        {
            _accessTokenManager.CancelCurrent();
            _idTokenManager.CancelCurrent();
            return false;
        }
        if(grant.AuthCodeExp < DateTime.Now)
            return false;
        return true;
    }
}
