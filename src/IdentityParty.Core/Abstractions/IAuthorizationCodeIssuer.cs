using IdentityParty.Core.Entities;

namespace IdentityParty.Core;

public interface IAuthorizationCodeIssuer
{
    string Issue();
}
