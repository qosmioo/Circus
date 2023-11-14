using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Models;
using Circus.Database.Repositories.Converters;
using CoreHall = Circus.Core.Models.Hall;

namespace Circus.Database.Repositories;

public class HallRepository : IHallRepository
{
    private readonly CircusContext _dbContext;

    public HallRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddHallAsync(Guid id, string name)
    {
        await _dbContext.Halls.AddAsync(new Hall(id, name));
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CoreHall>> GetHallsAsync()
    {
        var halls = await _dbContext.Halls
            .AsNoTracking()
            .Include(h => h.Sectors)
            .Include(h => h.Sessions)
            .ToListAsync();

        return halls.Select(HallConverter.ConvertHallToCore).ToList()!;
    }

    public async Task<CoreHall?> FindHallAsync(Guid id)
    {
        var hall = await _dbContext.Halls
            .Include(h => h.Id == id)
            .FirstOrDefaultAsync(h => h.Id == id);
        
        return HallConverter.ConvertHallToCore(hall);
    }

    public async Task UpdateHallAsync(Guid id, string name)
    {
        var hall = await _dbContext.Halls.FindAsync(id);

        if (hall == null)
            throw new InvalidOperationException($"Hall with id: {id} not found.");

        hall.Name = name;

        await _dbContext.SaveChangesAsync();
    }

    public Task<bool> ExistAsync(Guid id)
    {
        return _dbContext.Halls.AnyAsync(h => h.Id == id);
    }

    public async Task<CoreHall> RemoveHallAsync(Guid hallId)
    {
        var hall = await _dbContext.Halls.FirstOrDefaultAsync(h => h.Id == hallId);

        if (hall == null)
            throw new InvalidOperationException($"Hall with id: {hallId} not found.");

        _dbContext.Remove(hall);
        await _dbContext.SaveChangesAsync();

        return HallConverter.ConvertHallToCore(hall)!;
    }
}