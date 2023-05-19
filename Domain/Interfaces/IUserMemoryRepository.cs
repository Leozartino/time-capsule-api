using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserMemoryRepository
{
    Task<UserMemory>? GetUserMemory(Guid id);
    Task<IReadOnlyList<UserMemory>> GetUserMemories();
    Task<UserMemory> AddUserMemory(UserMemory memory);
    Task<UserMemory> PatchUserMemory(UserMemory userMemory, string coverUrl, string content, bool isPublic);
    Task<bool> DeleteUserMemory(Guid id);
    
}