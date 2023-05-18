using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserMemoryRepository: IUserMemoryRepository
{
    private readonly StoreContext _context;
    
    public UserMemoryRepository(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<UserMemory>? GetUserMemory(Guid id)
    {
        return await _context.Memories
            .Include(m => m.User)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public async Task<IReadOnlyList<UserMemory>> GetUserMemories()
    {
        return await _context.Memories
            .Include(m => m.User)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }
    
    
    
    
}