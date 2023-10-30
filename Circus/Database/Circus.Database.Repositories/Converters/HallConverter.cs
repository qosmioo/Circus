using System.Collections.Generic;
using System.Linq;
using CoreHall = Circus.Core.Models.Hall;
using DbHall = Circus.Database.Models.Hall;
using CoreSession = Circus.Core.Models.Session;
using CoreSector = Circus.Core.Models.Sector;

namespace Circus.Database.Repositories.Converters;

public static class HallConverter
{
    public static CoreHall? ConvertHallToCore(DbHall? dbHall)
    {
        if (dbHall is null)
            return null;

        var sessions = dbHall.Sessions is null
            ? new List<CoreSession>()
            : dbHall.Sessions.Select(SessionConverter.ConvertSessionToCore).ToList()!;

        var sectors = dbHall.Sectors is null
            ? new List<CoreSector>()
            : dbHall.Sectors.Select(SectorConverter.ConvertSectorToCore).ToList()!;

        return new CoreHall(dbHall.Id, dbHall.Name, sessions, sectors);
    }

    public static DbHall? ConvertHallToDb(CoreHall? coreHall)
    {
        if (coreHall is null)
            return null;

        return new DbHall(coreHall.Id, coreHall.Name);
    }
}