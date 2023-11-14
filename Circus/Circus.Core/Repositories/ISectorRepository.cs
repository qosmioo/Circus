using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface ISectorRepository
{
    Task AddSectorAsync(Guid id, Guid hallId, string name);

    Task<List<Sector>> GetSectorsAsync();

    Task<Sector?> FindSectorAsync(Guid id);

    Task<Sector> RemoveSectorAsync(Guid id);

    Task UpdateSectorAsync(Guid id, Guid hallId, string name);

    Task<bool> ExistAsync(Guid id);
}