using System.Collections.Generic;
using System.Linq;
using CoreActor = Circus.Core.Models.Actor;
using DbActor = Circus.Database.Models.Actor;
using CoreActorShow = Circus.Core.Models.ActorShow;

namespace Circus.Database.Repositories.Converters;

public static class ActorConverter
{
    public static CoreActor? ConvertActorToCore(DbActor? dbActor)
    {
        if (dbActor is null)
            return null;

        var actorShows = dbActor.ActorShows is null
            ? new List<CoreActorShow>()
            : dbActor.ActorShows.Select(ActorShowConverter.ConvertActorShowToCore).ToList()!;

        return new CoreActor(dbActor.Id, dbActor.Name, dbActor.Description, dbActor.AvatarId, actorShows);
    }
}