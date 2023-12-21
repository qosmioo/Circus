using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class ShowConverter
{
    public static Show? ConvertShowToDto(Core.Models.Show? coreShow)
    {
        if (coreShow == null)
            return null;

        var dtoFeedbacks = coreShow.Feedbacks.Select(FeedbackConverter.ConvertFeedbackToDto).ToList();
        var dtoSessions = coreShow.Sessions.Select(SessionConverter.ConvertSessionToDto).ToList();
        var dtoActorShows = coreShow.ActorShows.Select(ActorShowConverter.ConvertActorShowToDto).ToList();

        return new Show(coreShow.Id, coreShow.Name, coreShow.Description, coreShow.Duration,
            coreShow.PosterId, dtoFeedbacks, dtoSessions, dtoActorShows);
    }
    
    public static Core.Models.Show? ConvertShowToCore(Show? dtoShow)
    {
        if (dtoShow == null)
            return null;

        var coreFeedbacks = dtoShow.Feedbacks.Select(FeedbackConverter.ConvertFeedbackToCore).ToList();
        var coreSessions = dtoShow.Sessions.Select(SessionConverter.ConvertSessionToCore).ToList();
        var coreActorShows = dtoShow.ActorShows.Select(ActorShowConverter.ConvertActorShowToCore).ToList();

        return new Core.Models.Show(dtoShow.Id, dtoShow.Name, dtoShow.Description, dtoShow.Duration,
            dtoShow.PosterId, coreFeedbacks!, coreSessions!, coreActorShows!);
    }
}