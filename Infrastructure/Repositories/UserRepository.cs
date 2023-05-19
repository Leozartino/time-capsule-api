using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly StoreContext _context;
    
    public UserRepository(StoreContext context)
    {
        _context = context;
    }

    
    public async Task<User>? GetUser(Guid id)
    {
        return await _context.Users.SingleOrDefaultAsync(user => user.Id == id);
    }

    public async Task<IReadOnlyList<User>> GetUsers()
    {
        return await _context.Users
            .Include(user => user.UserMemories)    
            .OrderBy(user => user.Name)    
            .ToListAsync();
    }

    public async Task<User> AddUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

        if (user is null) return false;
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;

    }

    public async Task<bool> PatchUser(User user, string? name, string? principalName, string? avatarUrl)
    {
        if (!string.IsNullOrWhiteSpace(name) && user.Name != name)
        {
            user.Name = name;
        }
        
        if (!string.IsNullOrWhiteSpace(principalName)  && user.PrincipalName != principalName)
        {
            user.PrincipalName = principalName;
        }
        
        if (!string.IsNullOrWhiteSpace(avatarUrl)  && user.AvatarUrl != avatarUrl)
        {
            user.AvatarUrl = avatarUrl;
        }

        await _context.SaveChangesAsync();
        return true;
    }

}