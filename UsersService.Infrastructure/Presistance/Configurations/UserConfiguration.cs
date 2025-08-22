using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersService.Domain.Entities;

namespace UsersService.Infrastructure.Presistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(45);

        builder.OwnsOne(u => u.Email, b =>
            {
                b.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired();

                b.HasIndex(e => e.Value).IsUnique();
            }
        );

        builder.HasIndex(u => u.SubjectId)
            .IsUnique();
        
        builder.Navigation(x => x.RequesterShip)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.AddresseeShip)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Ignore(u => u.Friendships);

    }
}