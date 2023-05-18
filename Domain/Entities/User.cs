namespace Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    
    public string PrincipalName { get; set; } = string.Empty;
    
    public int GithubId { get; set; }
    
    public string? AvatarUrl { get; set; } = string.Empty;
    
    public ICollection<UserMemory> UserMemories { get; set; }
}