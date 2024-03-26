using IdentityParty.Core.Entities;

namespace IdentityParty.Core.Abstractions;

public interface IIdTokenManager
{
    string Issue(Grant grant);
    void CancelCurrent();
    void ValidateCurrent();
}