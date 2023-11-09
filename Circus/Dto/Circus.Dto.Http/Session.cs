using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Circus.Dto.Http;

[DataContract]
public class Session
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [Required]
    [DataMember(Name = "showId")]
    public Guid ShowId { get; set; }
    
    [Required]
    [DataMember(Name = "hallId")]
    public Guid HallId { get; set; }
    
    [Required]
    [DataMember(Name = "startsAt")]
    public DateTimeOffset StartsAt { get; set; }

    [Required]
    [DataMember(Name = "tickets")]
    public List<Ticket> Tickets { get; set; }

    public Session(Guid id, 
        Guid showId, 
        Guid hallId, 
        DateTimeOffset startsAt, List<Ticket> tickets)
    {
        Id = id;
        ShowId = showId;
        HallId = hallId;
        StartsAt = startsAt;
        Tickets = tickets;
    }
    }