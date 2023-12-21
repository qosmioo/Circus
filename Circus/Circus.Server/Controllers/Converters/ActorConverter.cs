using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class ActorConverter
{
    public static Actor? ConvertActorToDto(Core.Models.Actor? coreActor)
    {
        if (coreActor == null)
            return null;
        
        var dtoActorShows = coreActor.ActorShows
            .Select(ActorShowConverter.ConvertActorShowToDto).ToList();

        return new Actor(coreActor.Id, 
            coreActor.Name, 
            coreActor.Description, 
            coreActor.AvatarId, 
            dtoActorShows!);
    }

    public static Core.Models.Actor? ConvertActorToCore(Actor? dtoActor)
    {
        if (dtoActor == null)
            return null;

        var dtoActorShows = dtoActor.ActorShows
            .Select(ActorShowConverter.ConvertActorShowToCore).ToList();

        return new Core.Models.Actor(dtoActor.Id, 
            dtoActor.Name, 
            dtoActor.Description, 
            dtoActor.AvatarId, 
            dtoActorShows!);
    }
    
}