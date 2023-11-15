using System;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface IFileRepository
{
    Task AddFileAsync(Guid id,
        byte[] data,
        string extension,
        string name);

    Task<File?> FindFileAsync(Guid id);

    Task<File> RemoveFileAsync(Guid id);

    Task<bool> ExistAsync(Guid fileId);
}