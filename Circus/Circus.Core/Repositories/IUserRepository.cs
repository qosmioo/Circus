using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(Guid id,
        string login,
        string password,
        string name,
        string role,
        Guid? avatarId = null);

    Task<List<User>> GetUsersAsync();

    Task<User> FindUserAsync(Guid id);

    Task<User> RemoveUserAsync(Guid id);

    Task<bool> ExistAsync(Guid id);

    Task UpdateUserAsync(Guid id);
}