using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class SessionConverter
{
    public static Session? ConvertSessionToDto(Core.Models.Session? session)
    {
        if (session == null)
            return null;

        var dtoTickets = session.Tickets.Select(TicketConverter.ConvertTicketToDto).ToList();
        
        return new Session(session.Id, session.ShowId, session.HallId, session.StartsAt, dtoTickets);
    }
}