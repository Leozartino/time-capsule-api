using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User>? GetUser(Guid id);
    Task<IReadOnlyList<User>> GetUsers();
    Task<User> AddUser(User user);
    Task<bool> DeleteUser(Guid id);
    Task<bool> PatchUser(User user, string? name, string? principalName, string? avatarUrl);
}