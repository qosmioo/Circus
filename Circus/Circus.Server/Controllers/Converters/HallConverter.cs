using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class HallConverter
{
    public static Hall? ConvertHallToDto(Core.Models.Hall? dtoHall)
    {
        if (dtoHall == null)
            return null;

        var dtoSectors = dtoHall.Sectors.Select(SectorConverter.ConvertSectorToDto).ToList();
        var dtoSessions = dtoHall.Sessions.Select(SessionConverter.ConvertSessionToDto).ToList();
        
        return new Hall(dtoHall.Id, dtoHall.Name, dtoSessions, dtoSectors);
    }

    public static Core.Models.Hall? ConvertHallToCore(Hall? coreHall)
    {
        if (coreHall == null)
            return null;
        
        var coreSectors = coreHall.Sectors.Select(SectorConverter.ConvertSectorToCore).ToList();
        var coreSessions = coreHall.Sessions.Select(SessionConverter.ConvertSessionToCore).ToList();

        return new Core.Models.Hall(coreHall.Id, coreHall.Name, coreSessions!, coreSectors!);
    }
}