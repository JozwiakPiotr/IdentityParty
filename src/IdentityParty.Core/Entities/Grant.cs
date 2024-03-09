namespace IdentityParty.Core.Entities;

public class Grant
{
    public Grant(Guid endUserId, Guid clientId, List<Scope> scopes)
    {
        EndUserId = endUserId;
        ClientId = clientId;
        Scopes = scopes;
    }

    public Guid EndUserId { get; set; }
    public Guid ClientId { get; set; }
    public List<Scope> Scopes { get; set; }
}