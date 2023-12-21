using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class FeedbackConverter
{
    public static Feedback? ConvertFeedbackToDto(Core.Models.Feedback? feedback)
    {
        if (feedback == null)
            return null;

        return new Feedback(feedback.Id, 
            feedback.Text, 
            feedback.ShowId, 
            feedback.UserId, 
            feedback.CreatedAt,
            feedback.Rating);
    }

    public static Core.Models.Feedback? ConvertFeedbackToCore(Feedback? feedback)
    {
        if (feedback == null)
            return null;

        return new Core.Models.Feedback(feedback.Id,
            feedback.Text,
            feedback.ShowId,
            feedback.UserId,
            feedback.CreatedAt,
            feedback.Rating);
    }
}