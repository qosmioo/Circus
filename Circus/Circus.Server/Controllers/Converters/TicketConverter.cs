using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class TicketConverter
{
    public static Ticket? ConvertTicketToDto(Core.Models.Ticket? ticket)
    {
        if (ticket == null)
            return null;

        return new Ticket(ticket.Id, 
            ticket.SeatId, 
            ticket.SessionId, 
            ticket.UserId, 
            ticket.Price, 
            ticket.IsAvailable);
    }
}