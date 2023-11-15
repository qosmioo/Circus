using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class RowConverter
{
    public static Row? ConvertRowToDto(Core.Models.Row? row)
    {
        if (row == null)
            return null;
        
        var dtoSeats = row.Seats.Select(SeatConverter.ConvertSeatToDto).ToList();

        return new Row(row.Id, row.SectorId, row.RowNumber, dtoSeats);
    }
}