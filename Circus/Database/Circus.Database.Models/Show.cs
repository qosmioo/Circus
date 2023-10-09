using System;
using System.Collections.Generic;

namespace Circus.Database.Models;

public class Show
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public Guid PosterId { get; set; }

    public Show(Guid id, 
        string name, 
        string description, 
        TimeSpan duration, 
        Guid posterId)
    {
        Id = id;
        Name = name;
        Description = description;
        Duration = duration;
        PosterId = posterId;
    }
    
    public ICollection<Feedback>? Feedbacks { get; set; }
    
    public ICollection<Session>? Sessions { get; set; }
    
    public ICollection<ActorShow>? ActorShows { get; set; }
}
