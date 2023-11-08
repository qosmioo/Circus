using System.Collections.Generic;
using System.Linq;
using CoreFeedback = Circus.Core.Models.Feedback;
using CoreSession = Circus.Core.Models.Session;
using CoreActorShow = Circus.Core.Models.ActorShow;
using CoreShow = Circus.Core.Models.Show;
using DbShow = Circus.Database.Models.Show;

namespace Circus.Database.Repositories.Converters;

public static class ShowConverter
{
    public static CoreShow? ConvertShowToCore(DbShow? dbShow)
    {
        if (dbShow is null)
            return null;

        var feedbacks = dbShow.Feedbacks is null
            ? new List<CoreFeedback>()
            : dbShow.Feedbacks.Select(FeedbackConverter.ConvertFeedbackToCore).ToList()!;

        var sessions = dbShow.Sessions is null
            ? new List<CoreSession>()
            : dbShow.Sessions.Select(SessionConverter.ConvertSessionToCore).ToList()!;

        var actorShows = dbShow.ActorShows is null
            ? new List<CoreActorShow>()
            : dbShow.ActorShows.Select(ActorShowConverter.ConvertActorShowToCore).ToList()!;

        return new CoreShow(dbShow.Id,
            dbShow.Name,
            dbShow.Description,
            dbShow.Duration,
            dbShow.PosterId,
            feedbacks, 
            sessions, 
            actorShows);
    }

    public static DbShow? ConvertShowToDb(CoreShow? coreShow)
    {
        if (coreShow is null)
            return null;

        return new DbShow(coreShow.Id, 
            coreShow.Name, 
            coreShow.Description, 
            coreShow.Duration, 
            coreShow.PosterId);
    }
}