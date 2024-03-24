using IdentityParty.Core.Entities;

namespace IdentityParty.Core.Abstractions;

public interface IClientStore
{
    Task<bool> Exists(Guid id);
}