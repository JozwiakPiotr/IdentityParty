using IdentityParty.Core.Entities;

namespace IdentityParty.Core.Abstractions;

public interface IScopeStore
{
    Task<Scope> GetAllByGrantId(Guid grantId);
}