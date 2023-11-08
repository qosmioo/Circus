using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Models;
using Circus.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;
using CoreSession = Circus.Core.Models.Session;


namespace Circus.Database.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly CircusContext _dbContext;

    public SessionRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSessionAsync(Guid id, Guid showId, Guid hallId, DateTimeOffset startsAt)
    {
        await _dbContext.Sessions.AddAsync(new Session(id, showId, hallId, startsAt));
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CoreSession>> GetSessionsAsync()
    {
        var sessions = await _dbContext.Sessions
            .AsNoTracking()
            .Include(s => s.Tickets)
            .ToListAsync();
        
        return sessions.Select(SessionConverter.ConvertSessionToCore).ToList()!;
    }

    public async Task<CoreSession?> FindSessionAsync(Guid id)
    {
        var session = await _dbContext.Sessions
            .AsNoTracking()
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == id);

        return SessionConverter.ConvertSessionToCore(session);
    }

    public async Task<CoreSession> RemoveSessionAsync(Guid id)
    {
        var session = await _dbContext.Sessions
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (session == null)
            throw new InvalidOperationException($"Session with id: {id} was not found");
        
        _dbContext.Sessions.Remove(session);
        
        await _dbContext.SaveChangesAsync();

        return SessionConverter.ConvertSessionToCore(session)!;
    }

    public Task<bool> ExistAsync(Guid id)
    {
        return _dbContext.Sessions.AnyAsync(s => s.Id == id);
    }
}