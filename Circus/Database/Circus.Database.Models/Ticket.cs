#nullable enable
using System;

namespace Circus.Database.Models;

public class Ticket
{
    public Guid Id { get; set; }
    
    public Guid SeatId { get; set; }
    
    public Guid SessionId { get; set; }
    
    public Guid UserId { get; set; }
    
    public int Price { get; set; }
    
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
    
    public Show? Show { get; set; }
    
    public User? User { get; set; }
    
    public Seat? Seat { get; set; }
}