using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Circus.Dto.Http;

[DataContract]
public class Actor
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
    
    [DataMember(Name = "avatarId")]
    public Guid? AvatarId { get; set; }
    
    [Required]
    [DataMember(Name = "actorShows")]
    public List<ActorShow> ActorShows { get; set; }

    public Actor(Guid id, string name, string description, Guid? avatarId, List<ActorShow> actorShows)
    {
        Id = id;
        Name = name;
        Description = description;
        AvatarId = avatarId;
        ActorShows = actorShows;
    }
}