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

    public async Task<List<CoreHall>> GetHalls()
    {
        var halls = await _dbContext.Halls
            .AsNoTracking()
            .Include(h => h.Sectors)
            .Include(h => h.Sessions)
            .ToListAsync();

        return halls.Select(HallConverter.ConvertHallToCore).ToList()!;
    }

    public async Task<CoreHall?> FindHall(Guid id)
    {
        var hall = await _dbContext.Halls
            .Include(h => h.Id == id)
            .FirstOrDefaultAsync(h => h.Id == id);
        
        return HallConverter.ConvertHallToCore(hall);
    }

    public Task<bool> ExistAsync(Guid id)
    {
        return _dbContext.Halls.AnyAsync(h => h.Id == id);
    }
}