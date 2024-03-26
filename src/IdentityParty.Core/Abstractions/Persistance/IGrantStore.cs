using IdentityParty.Core.Entities;

namespace IdentityParty.Core.Abstractions;

public interface IGrantStore
{
    Task<bool> Any(Grant grant);
    Task<bool> AnyWithClientIdAndAuthCode(Guid clientId, string authCode);
    Task<Grant> Get(Guid clientId, string authCode);
    Task Update(Grant grant);
}