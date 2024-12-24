using IdentityParty.Core.Entities;

namespace IdentityParty.Core.Abstractions;

public interface IGrantStore
{
    Task<bool> AnyAsync(Grant grant);
    Task<bool> AnyWithClientIdAndAuthCodeAsync(Guid clientId, string authCode);
    Task<Grant> GetAsync(Guid clientId, string authCode);
    Task UpdateAsync(Grant grant);
}