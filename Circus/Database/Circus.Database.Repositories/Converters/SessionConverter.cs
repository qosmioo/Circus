using System.Collections.Generic;
using System.Linq;
using CoreSession = Circus.Core.Models.Session;
using DbSession = Circus.Database.Models.Session;
using CoreTicket = Circus.Core.Models.Ticket;
namespace Circus.Database.Repositories.Converters;

public static class SessionConverter
{
    public static CoreSession? ConvertSessionToCore(DbSession? dbSession)
    {
        if (dbSession is null)
            return null;

        var tickets = dbSession.Tickets is null
            ? new List<CoreTicket>()
            : dbSession.Tickets.Select(TicketConverter.ConvertTicketToCore).ToList()!;

        return new CoreSession(dbSession.Id, 
            dbSession.ShowId, 
            dbSession.HallId, 
            dbSession.StartsAt, 
            tickets);
    }

    public static DbSession? ConvertSessionToDb(CoreSession? coreSession)
    {
        if (coreSession is null)
            return null;

        return new DbSession(coreSession.Id, coreSession.ShowId, coreSession.HallId, coreSession.StartsAt);
    }
}