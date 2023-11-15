using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Circus.Dto.Http;

[DataContract]
public class Row        
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [Required]
    [DataMember(Name = "sectorId")]  
    public Guid SectorId { get; set; }
    
    [Required]
    [DataMember(Name = "rowNumber")]
    public int RowNumber { get; set; }
    
    [Required]
    [DataMember(Name = "seats")]
    public List<Seat> Seats { get; set; }

    public Row(Guid id, Guid sectorId, int rowNumber, List<Seat?> seats)
    {
        Id = id;
        SectorId = sectorId;
        RowNumber = rowNumber;
        Seats = seats;
    }
}