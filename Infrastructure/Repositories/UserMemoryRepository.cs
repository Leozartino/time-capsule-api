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
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public async Task<IReadOnlyList<UserMemory>> GetUserMemories()
    {
        return await _context.Memories
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<UserMemory> AddUserMemory(UserMemory memory)
    {
        _context.Memories.Add(memory);
        await _context.SaveChangesAsync();
        
        var createdUserMemory = await _context.Memories
            .SingleAsync(um => um.Id == memory.Id);
        
        return createdUserMemory;
    }
}