using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersService.Domain.Entities;
using UsersService.Domain.Enums;

namespace UsersService.Infrastructure.Presistance.Configurations;

public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.HasKey(f => f.Id);

        builder.HasIndex(f => new { f.RequesterId, f.AddresseeId })
            .IsUnique();

        builder.HasCheckConstraint("chk_self_friendship", 
            "\"RequesterId\" <> \"AddresseeId\"");

        builder.Property(f => f.CreatedAt)
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'")
            .ValueGeneratedOnAdd();

        builder.Property(f => f.AcceptedAt)
            .HasColumnType("timestamptz");

        builder.Property(f => f.Status)
            .HasConversion<string>()
            .HasDefaultValue(FriendStatus.Pending)
            .IsRequired();

        builder.HasOne(f => f.Requester)
            .WithMany(nameof(User.RequesterShip))
            .HasForeignKey(f => f.RequesterId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(f => f.Addressee)
            .WithMany(nameof(User.AddresseeShip))
            .HasForeignKey(f => f.AddresseeId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}