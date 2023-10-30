using System.Collections.Generic;
using System.Linq;
using DbSector = Circus.Database.Models.Sector;
using CoreSector = Circus.Core.Models.Sector;
using CoreRow = Circus.Core.Models.Row;


namespace Circus.Database.Repositories.Converters;

public static class SectorConverter
{
    public static CoreSector? ConvertSectorToCore(DbSector? dbSector)
    {
        if (dbSector is null)
            return null;

        var rows = dbSector.Rows is null
            ? new List<CoreRow>()
            : dbSector.Rows.Select(RowConverter.ConvertRowToCore).ToList()!;

        return new CoreSector(dbSector.Id, dbSector.HallId, dbSector.Name, rows);
    }

    public static DbSector? ConvertSectorToDb(CoreSector? coreSector)
    {
        if (coreSector is null)
            return null;

        return new DbSector(coreSector.Id, coreSector.HallId, coreSector.Name);
    }
}