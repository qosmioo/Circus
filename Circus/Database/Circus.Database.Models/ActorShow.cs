using System;

namespace Circus.Database.Models;

public class ActorShow
{
    public Guid Id { get; set; }
    
    public Guid ShowId { get; set; }
    
    public Guid ActorId { get; set; }
    
    public string Role { get; set; }

    public ActorShow(Guid id, 
        Guid showId, 
        Guid actorId, 
        string role)
    {
        Id = id;
        ShowId = showId;
        ActorId = actorId;
        Role = role;
    }
}
