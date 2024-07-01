using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniBlog.Core.Domain.Advertisements.Entities;
using System.Reflection.Emit;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.Advertisements.Configs;

public class AdminConfig : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.Property(c => c.BusinessId).IsRequired();
        builder.Property(c => c.Course).IsRequired();
        builder.HasIndex(c => c.BusinessId).IsUnique();

        

    }
}
