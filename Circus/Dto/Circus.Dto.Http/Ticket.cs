using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Circus.Core.Models;

namespace Circus.Dto.Http;

[DataContract]
public class Ticket
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [Required]
    [DataMember(Name = "seatId")]
    public Guid SeatId { get; set; }
    
    [Required]
    [DataMember(Name = "sessionId")]
    public Guid SessionId { get; set; }
    
    [Required]
    [DataMember(Name = "userId")]
    public Guid UserId { get; set; }
    
    [Required]
    [DataMember(Name = "price")]
    public int Price { get; set; }
    
    [Required]
    [DataMember(Name = "isAvailable")]
    public bool IsAvailable { get; set; }

    public Ticket(Guid id, 
        Guid seatId, 
        Guid sessionId, 
        Guid userId, 
        int price, 
        bool isAvailable)
    {
        Id = id;
        SeatId = seatId;
        SessionId = sessionId;
        UserId = userId;
        Price = price;
        IsAvailable = isAvailable;
    }
}