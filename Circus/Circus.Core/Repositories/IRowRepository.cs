using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface IRowRepository
{
    Task AddRowRepository(Guid id, Guid sectorId, int rowNumber);

    Task<List<Row>> GetRowsAsync();

    Task<Row?> FindRowAsync(Guid id);

    Task<Row> RemoveRowAsync(Guid id);

    Task<bool> ExistAsync(Guid id);
}