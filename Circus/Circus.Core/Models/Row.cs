using System;
using System.Collections.Generic;

namespace Circus.Core.Models;

public class Row
{
    public Guid Id { get; set; }
    
    public Guid SectorId { get; set; }
    
    public int RowNumber { get; set; }
    
    public Sector? Sector { get; set; }
    
    public List<Seat>? Seats { get; set; }

    public Row(Guid id, Guid sectorId, int rowNumber, Sector? sector, List<Seat> seats)
    {
        Id = id;
        SectorId = sectorId;
        RowNumber = rowNumber;
        Sector = sector;
        Seats = seats;
    }
}