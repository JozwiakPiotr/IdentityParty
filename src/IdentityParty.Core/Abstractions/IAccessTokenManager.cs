using IdentityParty.Core.Entities;

namespace IdentityParty.Core;

public interface IAccessTokenManager
{
    string IssueAccessToken(Grant grant);
    string IssueRefreshToken(Grant grant);
    string Refresh(string accessToken);
    void CancelCurrent();
    void ValidateCurrent();
}
