namespace IdentityParty.Core.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string Secret { get; set; }
    public string AuthorizationCode { get; set; }
}