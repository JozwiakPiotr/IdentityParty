namespace IdentityParty.Core.Entities;

public class Grant
{
    public Guid EndUserId { get; set; }
    public Guid ClientId { get; set; }
    public List<Scope> Scopes { get; set; }
}