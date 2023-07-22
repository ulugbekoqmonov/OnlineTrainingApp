using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastucture.DataAccess.Configurations;

public class PermissionConfiguration
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(per => per.Id);

        builder.Property(per => per.PermissionName)
            .IsRequired()
            .HasMaxLength(50);
    }
}
