#nullable enable
using System;
using System.Collections.Generic;

namespace Circus.Core.Models;

public class Show
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public Guid PosterId { get; set; }
    
    public List<Feedback> Feedbacks { get; set; }
    
    public List<Session> Sessions { get; set; }
    
    public List<ActorShow> ActorShows { get; set; }

    public Show(Guid id, 
        string name, 
        string description, 
        TimeSpan duration, 
        Guid posterId, 
        List<Feedback> feedbacks, 
        List<Session> sessions, 
        List<ActorShow> actorShows)
    {
        Id = id;
        Name = name;
        Description = description;
        Duration = duration;
        PosterId = posterId;
        Feedbacks = feedbacks;
        Sessions = sessions;
        ActorShows = actorShows;
    }
}