using IdentityParty.Core.Entities;

namespace IdentityParty.Core;

//TODO: implement real authorization code generator
public class DummyAuthCodeGenerator : IAuthorizationCodeIssuer
{
    public string Generate(Grant grant)
    {
        var bytes = new Byte[64];
        Random.Shared.NextBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}
