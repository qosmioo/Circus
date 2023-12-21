using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Models;
using Circus.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;
using CoreShow = Circus.Core.Models.Show;

namespace Circus.Database.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly CircusContext _dbContext;

    public ShowRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddShowAsync(Guid id, string name, string description, TimeSpan duration, Guid posterId)
    {
        await _dbContext.Shows.AddAsync(new Show(id, name, description, duration, posterId));
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CoreShow>> GetShowsAsync() // добавить инклюды 
    {
        var shows = await _dbContext.Shows
            .AsNoTracking()
            .Include(s => s.Sessions)
            .ToListAsync();
        
        return shows.Select(ShowConverter.ConvertShowToCore).ToList()!;
    }

    public async Task<CoreShow?> FindShowAsync(Guid id)
    {
        var show = await _dbContext.Shows
            .AsNoTracking()
            .Include(s => s.Sessions)
            .FirstOrDefaultAsync(s => s.Id == id);

        return ShowConverter.ConvertShowToCore(show);
    }

    public async Task<CoreShow> RemoveShowAsync(Guid id)
    {
        var show = await _dbContext.Shows
            .Include(s => s.Sessions)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (show == null)
            throw new InvalidOperationException($"Show with id: {id} was not found");
        
        _dbContext.Shows.Remove(show);
        
        await _dbContext.SaveChangesAsync();

        return ShowConverter.ConvertShowToCore(show)!;
    }

    public async Task UpdateShowAsync(Guid id, string name, string description, TimeSpan duration, Guid posterId)
    {
        var show = await _dbContext.Shows.FindAsync(id);

        if (show == null)
            throw new InvalidOperationException($"Feedback with id: {id} not found");

        show.Id = id;
        show.Name = name;
        show.Description = description;
        show.Duration = duration;
        show.PosterId = posterId;

        await _dbContext.SaveChangesAsync();
    }

    public Task<bool> ExistAsync(Guid id)
    {
        return _dbContext.Shows.AnyAsync(s => s.Id == id);
    }
}