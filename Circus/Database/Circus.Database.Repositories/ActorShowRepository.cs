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

    public async Task UpdateActorShowAsync(Guid id, Guid showId, Guid actorId, string role)
    {
        var actorShow = await _dbContext.ActorShows.FindAsync(id);

        if (actorShow == null)
            throw new InvalidOperationException($"ActorShow with id: {id} not found");

        actorShow.ShowId = showId;
        actorShow.ActorId = actorId;
        actorShow.Role = role;

        await _dbContext.SaveChangesAsync();
    }
}