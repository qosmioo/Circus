using System;
using System.Collections.Generic;

namespace Circus.Core.Models;

public class Session
{
    public Guid Id { get; set; }
    
    public Guid ShowId { get; set; }
    
    public Guid HallId { get; set; }
    
    public DateTimeOffset StartsAt { get; set; }
    
    public List<Ticket>? Tickets { get; set; }

    public Session(Guid id, Guid showId, Guid hallId, DateTimeOffset startsAt, List<Ticket>? tickets)
    {
        Id = id;
        ShowId = showId;
        HallId = hallId;
        StartsAt = startsAt;
        Tickets = tickets;
    }
}