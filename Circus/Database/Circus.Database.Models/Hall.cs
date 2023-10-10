using System;
using System.Collections.Generic;

namespace Circus.Database.Models;

public class Hall
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public Hall(Guid id, string name)
    {
        this.Id = id;
        Name = name;
    }

    public ICollection<Session>? Session { get; set; }

    public ICollection<Sector>? Sectors { get; set; }
}