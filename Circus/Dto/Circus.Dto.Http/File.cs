using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Circus.Dto.Http;

[DataContract]
public class File
{
    [Required]
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [Required]
    [DataMember(Name = "data")]
    public byte[] Data { get; set; }
    
    [Required]
    [DataMember(Name = "extension")]
    public string Extension { get; set; }
    
    [Required]
    [DataMember(Name = "name")]
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