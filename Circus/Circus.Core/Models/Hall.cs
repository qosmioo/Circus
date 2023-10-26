using System;
using System.Collections.Generic;

namespace Circus.Core.Models;

public class Hall
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public List<Session>? Sessions { get; set; }

    public List<Sector>? Sectors { get; set; }

    public Hall(Guid id, string name, List<Session>? sessions, List<Sector>? sectors)
    {
        Id = id;
        Name = name;
        Sessions = sessions;
        Sectors = sectors;
    }
}