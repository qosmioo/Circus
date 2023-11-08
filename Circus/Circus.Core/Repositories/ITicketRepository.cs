using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface ITicketRepository
{
    Task AddTicketsAsync(Guid id, 
        Guid seatId, 
        Guid sessionId, 
        Guid userId, 
        int price, 
        bool isAvailable);

    Task<List<Ticket>> GetTicketsAsync();

    Task<Ticket?> FindTicketAsync(Guid id);

    Task<Ticket> RemoveTicketAsync(Guid id);

    Task<bool> ExistAsync(Guid id);
}