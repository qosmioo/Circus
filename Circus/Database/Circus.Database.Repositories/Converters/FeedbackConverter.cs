using CoreFeedback = Circus.Core.Models.Feedback;
using DbFeedback = Circus.Database.Models.Feedback;

namespace Circus.Database.Repositories.Converters;

public static class FeedbackConverter
{
    public static CoreFeedback? ConvertToCore(DbFeedback? dbFeedback)
    {
        if (dbFeedback is null)
            return null;

        return new CoreFeedback(dbFeedback.Id, 
            dbFeedback.Text, 
            dbFeedback.ShowId, 
            dbFeedback.UserId, 
            dbFeedback.CreatedAt,
            dbFeedback.Rating);
    }

    public static DbFeedback? ConvertToDb(CoreFeedback? coreFeedback)
    {
        if (coreFeedback is null)
            return null;

        return new DbFeedback(coreFeedback.Id, 
            coreFeedback.Text, 
            coreFeedback.ShowId, 
            coreFeedback.UserId,
            coreFeedback.CreatedAt, 
            coreFeedback.Rating);
    }
}