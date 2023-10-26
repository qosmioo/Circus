using System;
using System.Collections.Generic;

namespace Circus.Core.Models;

public class Sector
{
    public Guid Id { get; set; }
    
    public Guid HallId { get; set; }
    
    public string Name { get; set; }
    
    public Hall? Hall { get; set; }
    
    public List<Row>? Rows { get; set; }

    public Sector(Guid id, Guid hallId, string name, Hall hall, List<Row> rows)
    {
        Id = id;
        HallId = hallId;
        Name = name;
        Hall = hall;
        Rows = rows;
    }
}