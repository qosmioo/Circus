using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Models;
using Circus.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;
using CoreTicket = Circus.Core.Models.Ticket;

namespace Circus.Database.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly CircusContext _dbContext;

    public TicketRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddTicketsAsync(Guid id, Guid seatId, Guid sessionId, Guid userId, int price, bool isAvailable)
    {
        await _dbContext.Tickets.AddAsync(new Ticket(id, seatId, sessionId, userId, price, isAvailable));

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CoreTicket>> GetTicketsAsync()
    {
        var tickets = await _dbContext.Tickets
            .AsNoTracking()
            .ToListAsync();
        
        return tickets.Select(TicketConverter.ConvertTicketToCore).ToList()!;
    }

    public async Task<CoreTicket?> FindTicketAsync(Guid id)
    {
        var ticket = await _dbContext.Tickets
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        return TicketConverter.ConvertTicketToCore(ticket);
    }

    public async Task<CoreTicket> RemoveTicketAsync(Guid id)
    {
        var ticket = await _dbContext.Tickets
            .FirstOrDefaultAsync(s => s.Id == id);

        if (ticket == null)
            throw new InvalidOperationException($"Ticket with id: {id} was not found");
        
        _dbContext.Tickets.Remove(ticket);
        
        await _dbContext.SaveChangesAsync();

        return TicketConverter.ConvertTicketToCore(ticket)!;
    }

    public async Task UpdateTicketsAsync(Guid id, 
        Guid seatId, 
        Guid sessionId, 
        Guid userId, 
        int price, 
        bool isAvailable)
    {
        var ticket = await _dbContext.Tickets.FindAsync(id);

        if (ticket == null)
            throw new InvalidOperationException($"Ticket with id: {id} was not found");

        ticket.SeatId = seatId;
        ticket.SessionId = sessionId;
        ticket.UserId = userId;
        ticket.Price = price;
        ticket.IsAvailable = isAvailable;
    }

    public Task<bool> ExistAsync(Guid id)
    {
        return _dbContext.Tickets.AnyAsync(t => t.Id == id);
    }
}