using File = Circus.Dto.Http.File;

namespace Circus.Server.Controllers.Converters;

public static class FileConverter
{
    public static File? ConvertFileToDto(Core.Models.File? dtoFile)
    {
        if (dtoFile is null)
            return null;

        return new File(dtoFile.Id,
            dtoFile.Data,
            dtoFile.Extension,
            dtoFile.Name);
    }
    
    public static Core.Models.File? ConvertFileToCore(File? coreFile)
    {
        if (coreFile is null)
            return null;

        return new Core.Models.File(coreFile.Id,
            coreFile.Data,
            coreFile.Extension,
            coreFile.Name);
    }
}