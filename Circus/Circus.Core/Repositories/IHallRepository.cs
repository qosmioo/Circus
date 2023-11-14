using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface IHallRepository
{
    Task AddHallAsync(Guid id, string name);

    Task<List<Hall>> GetHallsAsync();

    Task<Hall?> FindHallAsync(Guid id);

    Task UpdateHallAsync(Guid id, string name);

    Task<bool> ExistAsync(Guid id);

    Task<Hall> RemoveHallAsync(Guid hallId);
}