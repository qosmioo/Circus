using System;
using System.Collections.Generic;

namespace Circus.Core.Models;

public class Sector
{
    public Guid Id { get; set; }
    
    public Guid HallId { get; set; }
    
    public string Name { get; set; }
    
    public List<Row> Rows { get; set; }

    public Sector(Guid id, Guid hallId, string name, List<Row> rows)
    {
        Id = id;
        HallId = hallId;
        Name = name;
        Rows = rows;
    }
}