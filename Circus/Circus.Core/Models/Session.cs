using System;
using System.Collections.Generic;

namespace Circus.Core.Models;

public class Session
{
    public Guid Id { get; set; }
    
    public Guid ShowId { get; set; }
    
    public Guid HallId { get; set; }
    
    public DateTimeOffset StartsAt { get; set; }
    
    public Show? Show { get; set; }
    
    public Hall? Hall { get; set; }
    
    public List<Ticket>? Tickets { get; set; }

    public Session(Guid id, Guid showId, Guid hallId, DateTimeOffset startsAt, Show? show, Hall hall, List<Ticket>? tickets)
    {
        Id = id;
        ShowId = showId;
        HallId = hallId;
        StartsAt = startsAt;
        Show = show;
        Hall = hall;
        Tickets = tickets;
    }
}