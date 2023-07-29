using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastucture.DataAccess.Configurations;

public class RoleConfiguration
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(role=>role.Id);

        builder.Property(role=>role.RoleName)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(role => role.RoleName)
            .IsUnique(true);
    }
}
