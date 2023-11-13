using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class ActorConverter
{
    public static Actor? ConvertActorToDto(Core.Models.Actor? actor)
    {
        if (actor == null)
            return null;
        
        var dtoActorShows = actor.ActorShows.Select(ActorShowConverter.ConvertActorShowToDto).ToList();

        return new Actor(actor.Id, actor.Name, actor.Description, actor.AvatarId, dtoActorShows);
    }
}