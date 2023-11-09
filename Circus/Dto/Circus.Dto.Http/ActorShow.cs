using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Circus.Dto.Http;

[DataContract]
public class ActorShow
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [Required]
    [DataMember(Name = "showId")]
    public Guid ShowId { get; set; }
    
    [Required]
    [DataMember(Name = "actorId")]
    public Guid ActorId { get; set; }
    
    [Required]
    [DataMember(Name = "role")]
    public string Role { get; set; }

    public ActorShow(Guid id, Guid showId, Guid actorId, string role)
    {
        Id = id;
        ShowId = showId;
        ActorId = actorId;
        Role = role;
    }
}