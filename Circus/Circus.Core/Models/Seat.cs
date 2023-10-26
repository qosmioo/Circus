using System;
using System.Collections.Generic;

namespace Circus.Core.Models;

public class Seat
{
    public Guid Id { get; set; }
    
    public Guid RowId { get; set; }
    
    public int SeatNumber { get; set; }
    
    public List<Ticket>? Tickets { get; set; }

    public Seat(Guid id, Guid rowId, int seatNumber, List<Ticket>? tickets)
    {
        Id = id;
        RowId = rowId;
        SeatNumber = seatNumber;
        Tickets = tickets;
    }
}