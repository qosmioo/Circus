using CoreActorShow = Circus.Core.Models.ActorShow;
using DbActorShow = Circus.Database.Models.ActorShow;

namespace Circus.Database.Repositories.Converters;

public static class ActorShowConverter
{
    public static CoreActorShow? ConvertActorShowToCore(DbActorShow? dbActorShow)
    {
        if (dbActorShow is null)
            return null;

        return new CoreActorShow(dbActorShow.ShowId, 
            dbActorShow.ShowId, 
            dbActorShow.ActorId, 
            dbActorShow.Role);
    }

    public static DbActorShow? ConvertActorShowToDb(CoreActorShow? coreActorShow)
    {
        if (coreActorShow is null)
            return null;

        return new DbActorShow(coreActorShow.Id, 
            coreActorShow.ShowId, 
            coreActorShow.ActorId, 
            coreActorShow.Role);
    }
}