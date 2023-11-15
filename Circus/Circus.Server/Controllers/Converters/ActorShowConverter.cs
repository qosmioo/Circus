using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class ActorShowConverter
{
    public static ActorShow? ConvertActorShowToDto(Core.Models.ActorShow? actorShow)
    {
        if (actorShow == null)
            return null;
        
        return new ActorShow(actorShow.Id, actorShow.ShowId, actorShow.ActorId, actorShow.Role);
    }
}