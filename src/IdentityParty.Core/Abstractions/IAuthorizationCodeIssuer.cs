using IdentityParty.Core.Entities;

namespace IdentityParty.Core;

public interface IAuthorizationCodeIssuer
{
    string Generate(Grant grant);
}
