using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Circus.Core.Models;

namespace Circus.Dto.Http;

[DataContract]
public class Hall
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [Required]
    [DataMember(Name = "name")]
    public string Name { get; set; }

    [Required]
    [DataMember(Name = "sessions")]
    public List<Session> Sessions { get; set; }

    [Required]
    [DataMember(Name = "sectors")]
    public List<Sector> Sectors { get; set; }
    
    public Hall(Guid id, string name, List<Session> sessions, List<Sector> sectors)
    {
        Id = id;
        Name = name;
        Sessions = sessions;
        Sectors = sectors;
    }
}