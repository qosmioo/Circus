using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class HallConverter
{
    public static Hall? ConvertHallToDto(Core.Models.Hall? hall)
    {
        if (hall == null)
            return null;

        var dtoSectors = hall.Sectors.Select(SectorConverter.ConvertSectorToDto).ToList();
        var dtoSessions = hall.Sessions.Select(SessionConverter.ConvertSessionToDto).ToList();
        
        return new Hall(hall.Id, hall.Name, dtoSessions, dtoSectors);
    }
}