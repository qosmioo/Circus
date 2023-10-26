using System;
using System.Collections.Generic;

namespace Circus.Core.Models;

public class Actor
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public Guid? AvatarId { get; set; }
    
    public List<ActorShow>? ActorShows { get; set; }

    public Actor(Guid id, string name, string description, Guid? avatarId, List<ActorShow>? actorShows)
    {
        Id = id;
        Name = name;
        Description = description;
        AvatarId = avatarId;
        ActorShows = actorShows;
    }
}