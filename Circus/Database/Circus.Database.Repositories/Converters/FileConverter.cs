using System.Data.Entity.Core.Common.CommandTrees;
using Microsoft.Extensions.FileProviders;
using CoreFile = Circus.Core.Models.File;
using DbFile = Circus.Database.Models.File;

namespace Circus.Database.Repositories.Converters;

public static class FileConverter
{
    public static CoreFile? ConvertFileToCore(DbFile? file)
    {
        return file == null ? null : new CoreFile(file.Id, file.Data, file.Extension, file.Name);
    }

    public static DbFile? ConvertFileToDb(CoreFile? file)
    {
        return file == null ? null : new DbFile(file.Id, file.Data, file.Extension, file.Name);
    }
    
}