using System;
using System.Collections.Generic;

namespace Circus.Core.Models;

public class User
{
    public Guid Id { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
    
    public Guid? AvatarId { get; set; }
    
    public string Role { get; set; }
    
    public List<Feedback>? Feedbacks { get; set; }
    
    public List<Ticket>? Tickets { get; set; }

    public User(Guid id, string login, string password, string name, Guid? avatarId, string role, List<Feedback>? feedbacks, List<Ticket>? tickets)
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