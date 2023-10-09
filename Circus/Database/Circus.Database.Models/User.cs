using System;
using System.Collections.Generic;

namespace Circus.Database.Models;

public class User
{
    public Guid Id { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
    
    public Guid AvatarId { get; set; }
    
    public string Role { get; set; }

    public User(Guid id, 
        string login, 
        string password, 
        string name, 
        Guid avatarId, 
        string role)
    {
        Id = id;
        Login = login;
        Password = password;
        Name = name;
        AvatarId = avatarId;
        Role = role;
    }
    
    public ICollection<Feedback> Feedbacks { get; set; }
    
    public ICollection<Ticket> Tickets { get; set; }
}
