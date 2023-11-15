using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Models;
using Circus.Database.Repositories.Converters;
using CoreFile = Circus.Core.Models.File;

namespace Circus.Database.Repositories;

public class FileRepository : IFileRepository
{
    private readonly CircusContext _dbContext;

    public FileRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddFileAsync(Guid id, byte[] data, string extension, string name)
    {
        await _dbContext.Files.AddAsync(new File(id, data, extension, name));

        await _dbContext.SaveChangesAsync();
    }

    public async Task<CoreFile?> FindFileAsync(Guid id)
    {
        var file = await _dbContext.Files
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);

        return FileConverter.ConvertFileToCore(file);
    }

    public async Task<CoreFile> RemoveFileAsync(Guid id)
    {
        var file = await _dbContext.Files.FindAsync(id);

        if (file == null)
            throw new InvalidOperationException($"File with id: {id} not found.");

        _dbContext.Files.Remove(file);
        await _dbContext.SaveChangesAsync();

        return FileConverter.ConvertFileToCore(file)!;
    }

    public Task<bool> ExistAsync(Guid fileId)
    {
        return _dbContext.Files.AnyAsync(f => f.Id == fileId);
    }
}