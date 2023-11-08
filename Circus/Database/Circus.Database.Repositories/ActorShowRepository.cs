using System;
using System.Threading.Tasks;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Models;

namespace Circus.Database.Repositories;

public class ActorShowRepository : IActorShowRepository
{
    private readonly CircusContext _dbContext;

    public ActorShowRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddActorShowAsync(Guid id, Guid showId, Guid actorId, string role)
    {
        await _dbContext.ActorShows.AddAsync(new ActorShow(id, showId, actorId, role));

        await _dbContext.SaveChangesAsync();
    }
}