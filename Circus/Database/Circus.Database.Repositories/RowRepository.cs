using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Models;
using Circus.Database.Repositories.Converters;
using CoreRow = Circus.Core.Models.Row;

namespace Circus.Database.Repositories;

public class RowRepository : IRowRepository
{
    private readonly CircusContext _dbContext;

    public RowRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRowAsync(Guid id, Guid sectorId, int rowNumber)
    {
        await _dbContext.Rows.AddAsync(new Row(id, sectorId, rowNumber));

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CoreRow>> GetRowsAsync()
    {
        var rows = await _dbContext.Rows
            .AsNoTracking()
            .Include(r => r.Seats)
            .ToListAsync();
        return rows.Select(RowConverter.ConvertRowToCore).ToList()!;
    }

    public async Task<CoreRow?> FindRowAsync(Guid id)
    {
        var row = await _dbContext.Rows
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        return RowConverter.ConvertRowToCore(row);
    }

    public async Task<CoreRow> RemoveRowAsync(Guid id)
    {
        var row = await _dbContext.Rows
            .FirstOrDefaultAsync(r => r.Id == id);

        if (row == null)
            throw new InvalidOperationException($"Row with id: {id} was not found");
        _dbContext.Remove(row);

        await _dbContext.SaveChangesAsync();

        return RowConverter.ConvertRowToCore(row)!;
    }

    public async Task UpdateRowAsync(Guid id, Guid sectorId, int rowNumber)
    {
        var row = await _dbContext.Rows.FindAsync(id);

        if (row == null)
            throw new InvalidOperationException($"Row with id: {id} was not found");

        row.SectorId = sectorId;
        row.RowNumber = rowNumber;

        await _dbContext.SaveChangesAsync();
    }

    public Task<bool> ExistAsync(Guid id)
    {
        return _dbContext.Rows.AnyAsync(r => r.Id == id);
    }
}