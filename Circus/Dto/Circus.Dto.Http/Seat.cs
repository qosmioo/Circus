using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Circus.Dto.Http;

[DataContract]
public class Seat
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [Required]
    [DataMember(Name = "rowId")]
    public Guid RowId { get; set; }
    
    [Required]
    [DataMember(Name = "seatNumber")]
    public int SeatNumber { get; set; }
    
    [Required]
    [DataMember(Name = "tickets")]
    public List<Ticket> Tickets { get; set; }
    
    public Seat(Guid id, Guid rowId, int seatNumber, List<Ticket?> tickets)
    {
        Id = id;
        RowId = rowId;
        SeatNumber = seatNumber;
        Tickets = tickets;
    }
}