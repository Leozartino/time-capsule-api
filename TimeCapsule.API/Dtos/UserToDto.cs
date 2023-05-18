namespace TimeCapsule.API.Dtos;

public class UserToDto
{
    public string Name { get; set; } = string.Empty;
    
    public string PrincipalName { get; set; } = string.Empty;
    
    public int GithubId { get; set; }
    
    public string? AvatarUrl { get; set; } = string.Empty;
}