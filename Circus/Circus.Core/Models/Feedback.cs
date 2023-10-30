using System;

namespace Circus.Core.Models;

public class Feedback
{
    public Guid Id { get; set; }
    
    public string Text { get; set; }
    
    public Guid ShowId { get; set; }
    
    public Guid UserId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public int Rating { get; set; }

    public Feedback(Guid id, string text, Guid showId, Guid userId, DateTimeOffset createdAt, int rating)
    {
        Id = id;
        Text = text;
        ShowId = showId;
        UserId = userId;
        CreatedAt = createdAt;
        Rating = rating;
        }
}