using System;
using System.Collections.Generic;

namespace Circus.Database.Models;

public class Actor
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public Guid? AvatarId { get; set; }

    public Actor(Guid id, 
        string name, 
        string description, 
        Guid? avatarId)
    {
        Id = id;
        Name = name;
        Description = description;
        AvatarId = avatarId;
    }
    
    public ICollection<ActorShow> ActorShows { get; set; }
}
