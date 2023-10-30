using System.Collections.Generic;
using System.Linq;
using CoreUser = Circus.Core.Models.User;
using DbUser = Circus.Database.Models.User;
using CoreTicket = Circus.Core.Models.Ticket;
using CoreFeedback = Circus.Core.Models.Feedback;

namespace Circus.Database.Repositories.Converters;

public static class UserConverter
{
    public static CoreUser? ConvertUserToCore(DbUser? dbUser)
    {
        if (dbUser is null)
            return null;

        var feedbacks = dbUser.Feedbacks is null
            ? new List<CoreFeedback>()
            : dbUser.Feedbacks.Select(FeedbackConverter.ConvertFeedbackToCore).ToList()!;

        var tickets = dbUser.Tickets is null
            ? new List<CoreTicket>()
            : dbUser.Tickets.Select(TicketConverter.ConvertTicketToCore).ToList()!;

        return new CoreUser(dbUser.Id, 
            dbUser.Login, 
            dbUser.Password, 
            dbUser.Name, 
            dbUser.AvatarId, 
            dbUser.Role,
            feedbacks, tickets);
    }
}