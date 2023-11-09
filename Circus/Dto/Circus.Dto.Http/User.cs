using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Circus.Dro.Http;

[DataContract]
public class User
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [Required]
    [DataMember(Name = "login")]
    public string Login { get; set; }
    
    [Required]
    [DataMember(Name = "password")]
    public string Password { get; set; }
    
    [Required]
    [DataMember(Name = "name")]
    public string Name { get; set; }
    
    [Required]
    [DataMember(Name = "avatarId")]
    public Guid? AvatarId { get; set; }
    
    [Required]
    [DataMember(Name = "role")]
    public string Role { get; set; }
    
    [Required]
    [DataMember(Name = "feedbacks")]
    public List<Feedback> Feedbacks { get; set; }
    
    [Required]
    [DataMember(Name = "tickets")]
    public List<Ticket> Tickets { get; set; }

    public User(Guid id, string login, string password, string name, Guid? avatarId, string role, List<Feedback> feedbacks, List<Ticket> tickets)
    {
        Id = id;
        Login = login;
        Password = password;
        Name = name;
        AvatarId = avatarId;
        Role = role;
        Feedbacks = feedbacks;
        Tickets = tickets;
    }

    
}