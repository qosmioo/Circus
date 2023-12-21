using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class ActorShowConverter
{
    public static ActorShow? ConvertActorShowToDto(Core.Models.ActorShow? coreActorShow)
    {
        if (coreActorShow == null)
            return null;
        
        return new ActorShow(coreActorShow.Id, 
            coreActorShow.ShowId, 
            coreActorShow.ActorId, 
            coreActorShow.Role);
    }

    public static Core.Models.ActorShow? ConvertActorShowToCore(ActorShow? dtoActorShow)
    {
        if (dtoActorShow == null)
            return null;

        return new Core.Models.ActorShow(dtoActorShow.Id, 
            dtoActorShow.ShowId, 
            dtoActorShow.ActorId, 
            dtoActorShow.Role);
    }
}