using Circus.Core.Repositories;
using Circus.Server.Controllers.Converters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Circus.Server.Controllers;

[ApiController]
[Route("api/file")]
public class FilesController : ControllerBase
{
    private readonly IFileRepository _fileRepository;

    private readonly ILogger<FilesController> _logger;

    public FilesController(IFileRepository fileRepository, ILogger<FilesController> logger)
    {
        _fileRepository = fileRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("{fileId:guid}")]
    public async Task<IActionResult> GetFileAsync([FromRoute] Guid fileId)
    {
        try
        {
            var file = await _fileRepository.FindFileAsync(fileId);

            if (file == null)
                return NotFound();

            var dtoFile = FileConverter.ConvertFileToDto(file);

            return Ok(dtoFile);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", 
                nameof(GetFileAsync));

            return StatusCode(500);
        }
    }
    
    
    [HttpDelete("{fileId:guid}")]
    public async Task<IActionResult> DeleteFileAsync([FromRoute] Guid fileId)
    {
        try
        {
            await _fileRepository.RemoveFileAsync(fileId);

            return Ok();
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e, "File with id: {actorId} was not found", fileId);

            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method: {MethodName}.", nameof(DeleteFileAsync));

            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostFileAsync(IFormFile inputData, string name)
    {
        try
        {
            var fileId = Guid.NewGuid();
            var fileData = await GetContentFromFileAsync(inputData);

            await _fileRepository.AddFileAsync(fileId, fileData, "", name);

            return Ok(fileId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method: {MethodName}.", nameof(DeleteFileAsync));

            return StatusCode(500);
        }
        
    }
    
    private static async Task<byte[]> GetContentFromFileAsync(IFormFile inputFile)
    {
        await using var ms = new MemoryStream();
        await inputFile.CopyToAsync(ms);

        return ms.ToArray();
    }
}