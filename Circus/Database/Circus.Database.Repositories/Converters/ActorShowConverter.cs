using CoreActorShow = Circus.Core.Models.ActorShow;
using DbActorShow = Circus.Database.Models.ActorShow;

namespace Circus.Database.Repositories.Converters;

public class ActorShowConverter
{
    public static CoreActorShow? ConvertShowToCore(DbActorShow? dbActorShow)
    {
        if (dbActorShow is null)
            return null;

        return new CoreActorShow(dbActorShow.ShowId, 
            dbActorShow.ShowId, 
            dbActorShow.ActorId, 
            dbActorShow.Role);
    }

    public static DbActorShow? ConvertShowToDb(CoreActorShow? coreActorShow)
    {
        if (coreActorShow is null)
            return null;

        return new DbActorShow(coreActorShow.Id, 
            coreActorShow.ShowId, 
            coreActorShow.ActorId, 
            coreActorShow.Role);
    }
}