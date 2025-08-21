using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersService.Domain.Entities;

namespace UsersService.Infrastructure.Presistance.Configurations;

public class UserImageConfiguration : IEntityTypeConfiguration<UserImage>
{
    public void Configure(EntityTypeBuilder<UserImage> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.HasOne(i => i.User)
            .WithOne(u => u.UserImage)
            .HasForeignKey<UserImage>(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}