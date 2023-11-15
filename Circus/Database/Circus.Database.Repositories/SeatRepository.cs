using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Models;
using Circus.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;
using CoreSeat = Circus.Core.Models.Seat;

namespace Circus.Database.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly CircusContext _dbContext;

    public SeatRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSeatAsync(Guid id, Guid rowId, int seatNumber)
    {
        await _dbContext.Seats.AddAsync(new Seat(id, rowId, seatNumber));
    }

    public async Task<List<CoreSeat>> GetSeatsAsync()
    {
        var seats = await _dbContext.Seats
            .AsNoTracking()
            .Include(s => s.Tickets)
            .ToListAsync();
        
        return seats.Select(SeatConverter.ConvertSeatToCore).ToList()!;

    }

    public async Task<CoreSeat?> FindSeatAsync(Guid id)
    {
        var seat = await _dbContext.Seats
            .AsNoTracking()
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == id);

        return SeatConverter.ConvertSeatToCore(seat);
    }

    public async Task<CoreSeat> RemoveSeatAsync(Guid id)
    {
        var seat = await _dbContext.Seats
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (seat == null)
            throw new InvalidOperationException($"Seat with id: {id} was not found");
        
        _dbContext.Seats.Remove(seat);
        
        await _dbContext.SaveChangesAsync();

        return SeatConverter.ConvertSeatToCore(seat)!;
    }

    public async Task UpdateSeatAsync(Guid id, Guid rowId, int seatNumber)
    {
        var seat = await _dbContext.Seats.FindAsync(id);

        if (seat == null)
            throw new InvalidOperationException($"Seat with id: {id} was not found");

        seat.RowId = rowId;
        seat.SeatNumber = seatNumber;
    }

    public Task<bool> ExistAsync(Guid id)
    {
        return _dbContext.Seats.AnyAsync(s => s.Id == id);
    }
}