using System;

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
    
    public Ticket? Ticket { get; set; }
}