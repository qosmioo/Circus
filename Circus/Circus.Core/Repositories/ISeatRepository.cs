using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface ISeatRepository
{
    Task AddSeatAsync(Guid id, Guid rowId, int seatNumber);

    Task<List<Seat>> GetSeatsAsync();

    Task<Seat?> FindSeatAsync(Guid id);

    Task<Seat> RemoveSeatAsync(Guid id);

    Task UpdateSeatAsync(Guid id, Guid rowId, int seatNumber);
    
    Task<bool> ExistAsync(Guid id);
}