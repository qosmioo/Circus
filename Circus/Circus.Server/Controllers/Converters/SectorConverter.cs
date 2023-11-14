using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class SectorConverter
{
    public static Sector? ConvertSectorToDto(Core.Models.Sector? sector)
    {
        if (sector == null)
            return null;

        var dtoRows = sector.Rows.Select(RowConverter.ConvertRowToDto).ToList();

        return new Sector(sector.Id, sector.HallId, sector.Name, dtoRows);
    }
}