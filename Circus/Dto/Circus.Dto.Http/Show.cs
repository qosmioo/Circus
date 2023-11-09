using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Circus.Dto.Http;

[DataContract]
public class Show
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    
    [Required]
    [DataMember(Name = "name")]
    public string Name { get; set; }
    
    [Required]
    [DataMember(Name = "description")]
    public string Description { get; set; }
    
    
    [Required]
    [DataMember(Name = "duration")]
    public TimeSpan Duration { get; set; }
    
    [Required]
    [DataMember(Name = "posterId")]
    public Guid PosterId { get; set; }
    
    [Required]
    [DataMember(Name = "feedbacks")]
    public List<Feedback> Feedbacks { get; set; }
    
    [Required]
    [DataMember(Name = "sessions")]
    public List<Session> Sessions { get; set; }

    [Required]
    [DataMember(Name = "actorShows")]
    public List<ActorShow> ActorShows { get; set; }

    public Show(Guid id, 
        string name, 
        string description, 
        TimeSpan duration, 
        Guid posterId, List<Feedback> feedbacks, List<Session> sessions, List<ActorShow> actorShows)
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