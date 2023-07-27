using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastucture.DataAccess.Configurations;

public class UserConfiguration
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder
            .Property(u => u.FullName)
            .HasMaxLength(50);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .HasIndex(u => u.UserName)
            .IsUnique(true);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(14);
        builder.HasIndex(u => u.PhoneNumber)
            .IsUnique();

        builder
            .Property(u => u.ImageName)
            .HasMaxLength(100);
    }
}
