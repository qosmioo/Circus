using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface ISessionRepository
{
    Task AddSessionAsync(Guid id,
        Guid showId,
        Guid hallId,
        DateTimeOffset startsAt);

    Task<List<Session>> GetSessionsAsync();

    Task<Session?> FindSessionAsync(Guid id);

    Task<Session> RemoveSessionAsync(Guid id);

    Task UpdateSessionAsync(Guid id,
        Guid showId,
        Guid hallId,
        DateTimeOffset startsAt);

    Task<bool> ExistAsync(Guid id);
}