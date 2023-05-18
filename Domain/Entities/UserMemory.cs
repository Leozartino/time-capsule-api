using System.Text.Json.Serialization;

namespace Domain.Entities;

public class UserMemory : BaseEntity
{
    public string CoverUrl { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsPublic { get; set; } = false;
    [JsonIgnore]
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}