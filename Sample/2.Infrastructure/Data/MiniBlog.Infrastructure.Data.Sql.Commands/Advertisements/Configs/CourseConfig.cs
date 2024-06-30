using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniBlog.Core.Domain.Advertisements.Entities;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.Advertisements.Configs;

public class CourseConfig : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(c => c.BusinessId).IsRequired();
        builder.HasIndex(c => c.BusinessId).IsUnique();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
    }
}
