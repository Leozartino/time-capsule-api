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
        return await _context.Users.SingleAsync(user => user.Id == id);
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
}