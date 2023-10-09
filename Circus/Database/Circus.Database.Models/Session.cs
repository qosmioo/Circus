using System;
using System.Collections.Generic;

namespace Circus.Database.Models;

public class Session
{
    public Guid Id { get; set; }
    
    public Guid ShowId { get; set; }
    
    public Guid HallId { get; set; }
    
    public DateTimeOffset StartsAt { get; set; }

    public Session(Guid id, 
        Guid showId, 
        Guid hallId, 
        DateTimeOffset startsAt)
    {
        Id = id;
        ShowId = showId;
        HallId = hallId;
        StartsAt = startsAt;
    }
    
    public Show? Show { get; set; }
    
    public Hall? Hall { get; set; }
    
    public ICollection<Ticket>? Tickets { get; set; }
}