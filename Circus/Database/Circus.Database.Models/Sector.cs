using System;
using System.Collections.Generic;

namespace Circus.Database.Models;

public class Sector
{
    public Guid Id { get; set; }
    
    public Guid HallId { get; set; }
    
    public string Name { get; set; }

    public Sector(Guid id, Guid hallId, string name)
    {
        Id = id;
        HallId = hallId;
        Name = name;
    }
    
    public Hall? Hall { get; set; }
    
    public ICollection<Row> Rows { get; set; }
}