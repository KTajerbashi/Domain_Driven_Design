using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniBlog.Core.Domain.Advertisements.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.Advertisements.Configs;

public class AdvertisementConfig : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.Property(c => c.BusinessId).IsRequired();
        builder.HasIndex(c => c.BusinessId).IsUnique();
        builder.Property(c => c.Title).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Description).IsRequired().HasMaxLength(1000);

        builder.HasMany(a => a.Courses)
               .WithOne()
               .HasForeignKey("AdvertisementId")
               .OnDelete(DeleteBehavior.Cascade);


    

    }
}
