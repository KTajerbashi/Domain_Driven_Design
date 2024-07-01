using DDD.Infra.Data.Sql.Queries.Library;
using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Core.Domain.People.ValueObjects;
using MiniBlog.Infrastructure.Data.Sql.ValueConversions;

namespace MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext;

public class MiniBlogQueriesDbContext : BaseQueryDbContext
{
    public MiniBlogQueriesDbContext(DbContextOptions<MiniBlogQueriesDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Person> People { get; set; } = null!;
    public virtual DbSet<Advertisement> Advertisements { get; set; } = null!;
    public virtual DbSet<Course> Courses { get; set; } = null!;
    public virtual DbSet<Admin> Admins { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<FirstName>().HaveConversion<FirstNameConversion>();
        configurationBuilder.Properties<LastName>().HaveConversion<LastNameConversion>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //optionsBuilder.UseSqlServer("Server =TAJERBASHI; Database=MiniBlogDb;User Id = sa;Password=123123; MultipleActiveResultSets=true; Encrypt = false");
            //optionsBuilder.UseSqlServer("Server =172.20.1.20\\DEV; Database=MiniBlogDb;User Id = sa;Password=soft157703ware; MultipleActiveResultSets=true; Encrypt = false;");
        }
    }


}
