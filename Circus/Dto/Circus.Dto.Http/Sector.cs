using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Circus.Dto.Http;

[DataContract]
public class Sector
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [Required]
    [DataMember(Name = "hallId")]
    public Guid HallId { get; set; }
    
    [Required]
    [DataMember(Name = "name")]
    public string Name { get; set; }
    
    [Required]
    [DataMember(Name = "rows")]
    public List<Row> Rows { get; set; }

    public Sector(Guid id, Guid hallId, string name, List<Row> rows)
    {
        Id = id;
        HallId = hallId;
        Name = name;
        Rows = rows;
    }
}