using File = Circus.Dto.Http.File;

namespace Circus.Server.Controllers.Converters;

public static class FileConverter
{
    public static File? ConvertFileToDto(Core.Models.File? file)
    {
        return file == null ? null : new File(file.Id, file.Data, file.Extension, file.Name);
    }
}