using DDD.Infra.Data.Sql.Commands.Library;
using Extensions.Events.Abstractions;
using Extensions.Events.Outbox.Extensions.Events.Outbox.Dal.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Core.Domain.People.ValueObjects;
using MiniBlog.Infrastructure.Data.Sql.Commands.People.ValueConversions;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.DatabaseContext;

/// <summary>
/// بدون رویداد ها
/// </summary>
public class MiniBlogCommandsDbContext : BaseCommandDbContext

/// <summary>
/// 
/// </summary>
//public class MiniBlogCommandsDbContext : BaseOutboxCommandDbContext
{
    public MiniBlogCommandsDbContext(DbContextOptions<MiniBlogCommandsDbContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<FirstName>().HaveConversion<FirstNameConversion>();
        configurationBuilder.Properties<LastName>().HaveConversion<LastNameConversion>();
    }
    public DbSet<Person> People { get; set; }
}

public class MiniBlogCommandsDbContextFactory : IDesignTimeDbContextFactory<MiniBlogCommandsDbContext>
{
public MiniBlogCommandsDbContext CreateDbContext(string[] args)
{
    var optionsBuilder = new DbContextOptionsBuilder<MiniBlogCommandsDbContext>();
    optionsBuilder.UseSqlServer("Server =TAJERBASHI; Database=MiniBlogDb;User Id = sa;Password=123123; MultipleActiveResultSets=true; Encrypt = false");
    //optionsBuilder.UseSqlServer("Server =172.20.1.20\\DEV; Database=MiniBlogDb;User Id = sa;Password=soft157703ware; MultipleActiveResultSets=true; Encrypt = false");

    return new MiniBlogCommandsDbContext(optionsBuilder.Options);
}
}