using System;

namespace Circus.Core.Models;

public class ActorShow
{
    public Guid Id { get; set; }
    
    public Guid ShowId { get; set; }
    
    public Guid ActorId { get; set; }
    
    public string Role { get; set; }
    
    public Show? Show { get; set; }
    
    public Actor? Actor { get; set; }

    public ActorShow(Guid id, Guid showId, Guid actorId, string role, Show? show, Actor actor)
    {
        Id = id;
        ShowId = showId;
        ActorId = actorId;
        Role = role;
        Show = show;
        Actor = actor;
    }
}