using IdentityParty.Core.Entities;

namespace IdentityParty.Core;

public class DummyAuthCodeGenerator : IAuthorizationCodeIssuer
{
    public string Generate(Client client)
    {
        
        return Random.Shared.NextBytes()
    }
}
