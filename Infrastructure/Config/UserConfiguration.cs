using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.Name).IsRequired();
        builder.Property(user => user.PrincipalName).IsRequired();
        builder.Property(user => user.GithubId).IsRequired();
        builder.HasMany(user => user.UserMemories)
            .WithOne(memory => memory.User)
            .HasForeignKey(memory => memory.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}