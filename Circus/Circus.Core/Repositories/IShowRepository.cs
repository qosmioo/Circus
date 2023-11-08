using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface IShowRepository
{
    Task AddShowAsync(Guid id,
        string name,
        string description,
        TimeSpan duration,
        Guid posterId);

    Task<List<Show>> GetShowsAsync();

    Task<Show?> FindShowAsync(Guid id);

    Task<Show> RemoveShowAsync(Guid id);

    Task UpdateShowAsync(Guid id,
        string name,
        string description,
        TimeSpan duration,
        Guid posterId);

    Task<bool> ExistAsync(Guid id);
}