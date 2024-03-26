namespace IdentityParty.Core.Entities;

public class Grant
{
    public Grant(Guid endUserId, Guid clientId, List<Scope> scopes)
    {
        EndUserId = endUserId;
        ClientId = clientId;
        Scopes = scopes;
    }
    
    //TODO: maybe move to config
    private const int AuthCodeExpTimeInMinutes = 10;
    public Guid EndUserId { get; set; }
    public Guid ClientId { get; set; }
    public List<Scope> Scopes { get; set; }
    public string? AuthorizationCode { get; private set; }
    public DateTime? AuthCodeExp { get; private set; }
    public bool Used { get; set; }

    public void SetAuthCode(string authCode)
    {
        AuthorizationCode = authCode;
        //TODO: DateTime.Now vs DateTime.UtcNow
        AuthCodeExp = DateTime.Now.AddMinutes(AuthCodeExpTimeInMinutes);
    }
}