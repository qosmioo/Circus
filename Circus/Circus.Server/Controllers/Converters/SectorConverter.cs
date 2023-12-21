using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class SectorConverter
{
    public static Sector? ConvertSectorToDto(Core.Models.Sector? coreSector)
    {
        if (coreSector == null)
            return null;

        var dtoRows = coreSector.Rows.Select(RowConverter.ConvertRowToDto).ToList();

        return new Sector(coreSector.Id, coreSector.HallId, coreSector.Name, dtoRows);
    }
    
    public static Core.Models.Sector? ConvertSectorToCore(Sector? dtoSector)
    {
        if (dtoSector == null)
            return null;

        var coreRows = dtoSector.Rows.Select(RowConverter.ConvertRowToCore).ToList();

        return new Core.Models.Sector(dtoSector.Id, dtoSector.HallId, dtoSector.Name, coreRows!);
    }
}