using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Models;
using Circus.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;
using CoreActor = Circus.Core.Models.Actor;

namespace Circus.Database.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly CircusContext _dbContext;

    public ActorRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddActorAsync(Guid id, 
        string name, 
        string description, 
        Guid? avatarId = null)
    {
        await _dbContext.Actors.AddAsync(new Actor(id, name, description, avatarId));

        await _dbContext.SaveChangesAsync();
    }

    public async Task<CoreActor?> FindActorAsync(Guid id)
    {
        var actor = await _dbContext.Actors
            .AsNoTracking()
            .Include(a => a.ActorShows)
            .FirstOrDefaultAsync(a => a.Id == id);

        return ActorConverter.ConvertActorToCore(actor);
    }

    public async Task<List<CoreActor>> GetActorsAsync()
    {
        var actors = await _dbContext.Actors
            .AsNoTracking()
            .Include(a => a.ActorShows)
            .ToListAsync();

        return actors.Select(ActorConverter.ConvertActorToCore).ToList()!;
    }

    public async Task<CoreActor> RemoveActorAsync(Guid id)
    {
        var actor = await _dbContext.Actors
            .Include(a => a.ActorShows)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (actor == null)
            throw new InvalidOperationException($"Actor with id: {id} was not found");
        
        _dbContext.Actors.Remove(actor);
        await _dbContext.SaveChangesAsync();

        return ActorConverter.ConvertActorToCore(actor)!;
    }

    public Task<bool> ExistAsync(Guid id)
    {
        return _dbContext.Actors.AnyAsync(a => a.Id == id);
    }

    public async Task AddAvatarAsync(Guid actorId, Guid avatarId)
    {
        var actor = await _dbContext.Actors.FindAsync(actorId);
        
        if (actor == null)
            throw new InvalidEnumArgumentException($"Actor with id: {actorId} was not found");

        actor.AvatarId = avatarId;
        
        await _dbContext.SaveChangesAsync();
    }
}