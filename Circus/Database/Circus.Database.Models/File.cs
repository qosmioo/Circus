using System;

namespace Circus.Database.Models;

public class File
{
    public Guid Id { get; set; }
    
    public byte[] Data { get; set; }
    
    public string Extension { get; set; }
    
    public string Name { get; set; }

    public File(Guid id, 
        byte[] data, 
        string extension, 
        string name)
    {
        Id = id;
        Data = data;
        Extension = extension;
        Name = name;
    }
}