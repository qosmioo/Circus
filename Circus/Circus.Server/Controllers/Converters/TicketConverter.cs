using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class TicketConverter
{
    public static Ticket? ConvertTicketToDto(Core.Models.Ticket? coreTicket)
    {
        if (coreTicket == null)
            return null;

        return new Ticket(coreTicket.Id, 
            coreTicket.SeatId, 
            coreTicket.SessionId, 
            coreTicket.UserId, 
            coreTicket.Price, 
            coreTicket.IsAvailable);
    }
    
    public static Core.Models.Ticket? ConvertTicketToCore(Ticket? dtoTicket)
    {
        if (dtoTicket == null)
            return null;

        return new Core.Models.Ticket(dtoTicket.Id, 
            dtoTicket.SeatId, 
            dtoTicket.SessionId, 
            dtoTicket.UserId, 
            dtoTicket.Price, 
            dtoTicket.IsAvailable);
    }
}