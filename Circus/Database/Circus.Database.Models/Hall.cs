using System;
using System.Collections.Generic;

namespace Circus.Database.Models;

public class Hall
{
    public Guid id { get; set; }
    
    public string Name { get; set; }

    public Hall(Guid id, string name)
    {
        this.id = id;
        Name = name;
    }

    public Session? Session { get; set; }

    public ICollection<Sector>? Sectors { get; set; }
}