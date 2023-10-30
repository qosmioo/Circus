using System.Collections.Generic;
using System.Linq;
using CoreSeat = Circus.Core.Models.Seat;
using CoreRow = Circus.Core.Models.Row;
using DbRow = Circus.Database.Models.Row;

namespace Circus.Database.Repositories.Converters;

public static class RowConverter
{
    public static CoreRow? ConvertRowToCore(DbRow? dbRow)
    {
        if (dbRow is null)
            return null;

        var seats = dbRow.Seats is null
            ? new List<CoreSeat>()
            : dbRow.Seats.Select(SeatConverter.ConvertSeatToCore).ToList()!;

        return new CoreRow(dbRow.Id, dbRow.SectorId, dbRow.RowNumber, seats);
    }

    public static DbRow? ConvertRowToDb(CoreRow? coreRow)
    {
        if (coreRow is null)
            return null;

        return new DbRow(coreRow.Id, coreRow.SectorId, coreRow.RowNumber);
    }
}