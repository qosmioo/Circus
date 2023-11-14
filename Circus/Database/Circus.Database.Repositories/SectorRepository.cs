using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circus.Database.Models;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;
using CoreSector = Circus.Core.Models.Sector;

namespace Circus.Database.Repositories;

public class SectorRepository : ISectorRepository
{
    private readonly CircusContext _dbContext;

    public SectorRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSectorAsync(Guid id, Guid hallId, string name)
    {
        await _dbContext.Sectors.AddAsync(new Sector(id, hallId, name));
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CoreSector>> GetSectorsAsync()
    {
        var sectors = await _dbContext.Sectors
            .AsNoTracking()
            .Include(s => s.Rows)
            .ToListAsync();
        
        return sectors.Select(SectorConverter.ConvertSectorToCore).ToList()!;
    }

    public async Task<CoreSector?> FindSectorAsync(Guid id)
    {
        var sector = await _dbContext.Sectors
            .AsNoTracking()
            .Include(s => s.Rows)
            .FirstOrDefaultAsync(s => s.Id == id);

        return SectorConverter.ConvertSectorToCore(sector);
    }

    public async Task<CoreSector> RemoveSectorAsync(Guid id)
    {
        var sector = await _dbContext.Sectors
            .Include(s => s.Rows)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sector == null)
            throw new InvalidOperationException($"Sector with id: {id} was not found");
        
        _dbContext.Sectors.Remove(sector);
        
        await _dbContext.SaveChangesAsync();

        return SectorConverter.ConvertSectorToCore(sector)!;
    }

    public async Task UpdateSectorAsync(Guid id, Guid hallId, string name)
    {
        var sector = await _dbContext.Sectors.FindAsync(id);

        if (sector == null)
            throw new InvalidOperationException($"Sector with id: {id} was not found");
    }

    public Task<bool> ExistAsync(Guid id)
    {
        return _dbContext.Sectors.AnyAsync(s => s.Id == id);
    }
}