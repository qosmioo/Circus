using System.Collections.Generic;
using System.Linq;
using CoreSeat = Circus.Core.Models.Seat;
using DbSeat = Circus.Database.Models.Seat;
using CoreTicket = Circus.Core.Models.Ticket;

namespace Circus.Database.Repositories.Converters;

public static class SeatConverter
{
    public static CoreSeat? ConvertSeatToCore(DbSeat? dbSeat)
    {
        if (dbSeat is null)
            return null;
        
        var tickets = dbSeat.Tickets is null
            ? new List<CoreTicket>()
            : dbSeat.Tickets.Select(TicketConverter.ConvertTicketToCore).ToList()!;

        return new CoreSeat(dbSeat.Id, dbSeat.RowId, dbSeat.SeatNumber, tickets);
    }

    public static DbSeat? ConvertSeatToCore(CoreSeat? coreSeat)
    {
        if (coreSeat is null)
            return null;

        return new DbSeat(coreSeat.Id, coreSeat.RowId, coreSeat.SeatNumber);
    }
}