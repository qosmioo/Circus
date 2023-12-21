using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class RowConverter
{
    public static Row? ConvertRowToDto(Core.Models.Row? coreRow)
    {
        if (coreRow == null)
            return null;
        
        var dtoSeats = coreRow.Seats.Select(SeatConverter.ConvertSeatToDto).ToList();

        return new Row(coreRow.Id, coreRow.SectorId, coreRow.RowNumber, dtoSeats);
    }

    public static Core.Models.Row? ConvertRowToCore(Row? dtoRow)
    {
        if (dtoRow == null)
            return null;
        
        var coreSeats = dtoRow.Seats.Select(SeatConverter.ConvertSeatToCore).ToList();
        
        return new Core.Models.Row(dtoRow.Id, dtoRow.SectorId, dtoRow.RowNumber, coreSeats!);
    }
}