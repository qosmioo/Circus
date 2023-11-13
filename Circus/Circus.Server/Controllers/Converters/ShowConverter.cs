using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class ShowConverter
{
    public static Show? ConvertShowToDto(Core.Models.Show? show)
    {
        if (show == null)
            return null;

        var dtoFeedbacks = show.Feedbacks.Select(FeedbackConverter.ConvertFeedbackToDto).ToList();
        var dtoSessions = show.Sessions.Select(SessionConverter.ConvertSessionToDto).ToList();
        var dtoActorShows = show.ActorShows.Select(ActorShowConverter.ConvertActorShowToDto).ToList();

        return new Show(show.Id, show.Name, show.Description, show.Duration,
            show.PosterId, dtoFeedbacks, dtoSessions, dtoActorShows);
    }
}