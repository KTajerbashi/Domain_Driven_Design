using DDD.Infra.Data.Sql.Queries.Library;
using Microsoft.EntityFrameworkCore;
using MiniBlog.Infrastructure.Data.Sql.Queries.People;

namespace MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext;

public class MiniBlogQueriesDbContext : BaseQueryDbContext
{
    public MiniBlogQueriesDbContext(DbContextOptions<MiniBlogQueriesDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Person> People { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //optionsBuilder.UseSqlServer("Server =TAJERBASHI; Database=MiniBlogDb;User Id = sa;Password=123123; MultipleActiveResultSets=true; Encrypt = false");
            optionsBuilder.UseSqlServer("Server =172.20.1.20\\DEV; Database=MiniBlogDb;User Id = sa;Password=soft157703ware; MultipleActiveResultSets=true; Encrypt = false;");
        }
    }


}
