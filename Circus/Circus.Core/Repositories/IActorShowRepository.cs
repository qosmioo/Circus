using System;
using System.Threading.Tasks;

namespace Circus.Core.Repositories;

public interface IActorShowRepository
{
    Task AddActorShowAsync(Guid id,
        Guid showId,
        Guid actorId,
        string role);
}