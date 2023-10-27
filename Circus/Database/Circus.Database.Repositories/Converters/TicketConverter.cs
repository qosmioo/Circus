using DbTicket = Circus.Database.Models.Ticket;
using CoreTicket = Circus.Core.Models.Ticket;

namespace Circus.Database.Repositories.Converters;

public class TicketConverter
{
    public static CoreTicket? ConvertTicketToCore(DbTicket? dbTicket)
    {
        if (dbTicket is null)
            return null;

        return new CoreTicket(dbTicket.Id, 
            dbTicket.SeatId, 
            dbTicket.SessionId, 
            dbTicket.UserId,
            dbTicket.Price, 
            dbTicket.IsAvailable);
    }

    public static DbTicket? ConvertTicketToDb(CoreTicket? coreTicket)
    {
        if (coreTicket is null)
            return null;

        return new DbTicket(coreTicket.Id, 
            coreTicket.SeatId, 
            coreTicket.SessionId, 
            coreTicket.UserId, 
            coreTicket.Price,
            coreTicket.IsAvailable);
    }
}