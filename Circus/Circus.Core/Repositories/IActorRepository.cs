using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface IActorRepository
{
    Task AddActorAsync(Guid id, 
        string name, 
        string description, 
        Guid? avatarId = null);

    Task<Actor?> FindActorAsync(Guid id);

    Task<List<Actor>> GetActorsAsync();
    
    Task<Actor> RemoveActorAsync(Guid id);

    Task UpdateActorAsync(Guid id,
        string name,
        string description,
        Guid? avatarId = null);

    Task<bool> ExistAsync(Guid id);
    
    Task AddAvatarAsync(Guid actorId, Guid avatarId);
}
