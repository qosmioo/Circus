using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface IHallRepository
{
    Task AddHallAsync(Guid id, string name);

    Task<List<Hall>> GetHalls();

    Task<Hall?> FindHall(Guid id);

    Task<bool> ExistAsync(Guid id);

}