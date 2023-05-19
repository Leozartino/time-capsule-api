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
    
    public async Task<UserMemory> PatchUserMemory(UserMemory userMemory, string coverUrl, string content, bool isPublic)
    {
        
        if (!string.IsNullOrWhiteSpace(coverUrl) && userMemory.CoverUrl != coverUrl)
        {
            userMemory.CoverUrl = coverUrl;
        }
        
        if (!string.IsNullOrWhiteSpace(content) && userMemory.Content != content)
        {
            userMemory.Content = content;
        }
        
        if (userMemory.IsPublic != isPublic)
        {
            userMemory.IsPublic = isPublic;
        }

        await _context.SaveChangesAsync();
        return userMemory;
    }
    
    public async Task<bool> DeleteUserMemory(Guid id)
    {
        var userMemory = await _context.Memories.SingleOrDefaultAsync(um => um.Id == id);

        if (userMemory is null) return false;
        
        _context.Memories.Remove(userMemory);
        await _context.SaveChangesAsync();
        return true;

    }
}