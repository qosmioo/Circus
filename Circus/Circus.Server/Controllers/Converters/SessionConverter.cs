using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class SessionConverter
{
    public static Session? ConvertSessionToDto(Core.Models.Session? coreSession)
    {
        if (coreSession == null)
            return null;

        var dtoTickets = coreSession.Tickets.Select(TicketConverter.ConvertTicketToDto).ToList();
        
        return new Session(coreSession.Id, 
            coreSession.ShowId, 
            coreSession.HallId, 
            coreSession.StartsAt, 
            dtoTickets);
    }
    
    public static Core.Models.Session? ConvertSessionToCore(Session? dtoSession)
    {
        if (dtoSession == null)
            return null;

        var coreTickets = dtoSession.Tickets.Select(TicketConverter.ConvertTicketToCore).ToList();
        
        return new Core.Models.Session(dtoSession.Id, 
            dtoSession.ShowId, 
            dtoSession.HallId, 
            dtoSession.StartsAt, 
            coreTickets!);
    }
}