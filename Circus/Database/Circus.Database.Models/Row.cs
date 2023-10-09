using System;
using System.Collections.Generic;

namespace Circus.Database.Models;

public class Row
{
    public Guid Id { get; set; }
    
    public Guid SectorId { get; set; }
    
    public int RowNumber { get; set; }

    public Row(Guid id, Guid sectorId, int rowNumber)
    {
        Id = id;
        SectorId = sectorId;
        RowNumber = rowNumber;
    }
    
    public Sector? Sector { get; set; }
    
    public ICollection<Seat>? Seats { get; set; }
}