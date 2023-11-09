using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Circus.Dto.Http;

[DataContract]
public class Feedback
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [Required]
    [DataMember(Name = "text")]
    public string Text { get; set; }
    
    [Required]
    [DataMember(Name = "showId")]
    public Guid ShowId { get; set; }
    
    [Required]
    [DataMember(Name = "userId")]
    public Guid UserId { get; set; }
    
    [Required]
    [DataMember(Name = "createdAt")]
    public DateTimeOffset CreatedAt { get; set; }
    
    [Required]
    [DataMember(Name = "rating")]
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