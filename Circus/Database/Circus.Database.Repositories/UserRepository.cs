using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circus.Database.Models;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;
using CoreUser = Circus.Core.Models.User;


namespace Circus.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CircusContext _dbContext;

    public UserRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUserAsync(Guid id, string login, string password, string name, string role, Guid? avatarId = null)
    {
        await _dbContext.Users.AddAsync(new User(id, login, password, name, avatarId, role));
    }

    public async Task<List<CoreUser>> GetUsersAsync()
    {
        var users = await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();
        
        return users.Select(UserConverter.ConvertUserToCore).ToList()!;
    }

    public async Task<CoreUser?> FindUserAsync(Guid id)
    {
        var users = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return UserConverter.ConvertUserToCore(users);
    }

    public async Task<CoreUser> RemoveUserAsync(Guid id)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            throw new InvalidOperationException($"User with id: {id} was not found");
        
        _dbContext.Users.Remove(user);
        
        await _dbContext.SaveChangesAsync();

        return UserConverter.ConvertUserToCore(user)!;
    }

    public Task<bool> ExistAsync(Guid id)
    {
        return _dbContext.Users.AnyAsync(u => u.Id == id);
    }

    public async Task UpdateUserAsync(Guid id, 
        string login,
        string password,
        string name,
        string role,
        Guid? avatarId = null)
    {
        var user = await _dbContext.Users.FindAsync(id);

        if (user == null)
            throw new InvalidOperationException($"User with id: {id} not found");

        user.Id = id;
        user.Login = login;
        user.Password = password;
        user.Name = name;
        user.Role = role;
        user.AvatarId = avatarId;

        await _dbContext.SaveChangesAsync();
    }
}