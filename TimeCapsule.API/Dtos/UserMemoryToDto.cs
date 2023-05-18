namespace TimeCapsule.API.Dtos;

public class UserMemoryToDto
{
    public string CoverUrl { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsPublic { get; set; } 
    public Guid UserId { get; set; }
}