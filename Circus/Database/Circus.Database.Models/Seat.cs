using System;
using System.Collections.Generic;

namespace Circus.Database.Models;

public class Seat
{
    public Guid Id { get; set; }
    
    public Guid RowId { get; set; }
    
    public int SeatNumber { get; set; }

    public Seat(Guid id, Guid rowId, int seatNumber)
    {
        Id = id;
        RowId = rowId;
        SeatNumber = seatNumber;
    }
    
    public Row? Row { get; set; }
    
    public ICollection<Ticket>? Ticket { get; set; }
}