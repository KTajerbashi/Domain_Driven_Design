using BaseSource.Infrastructure.SQL.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.Infrastructure.SQL.Command.Common.Persistence;

public abstract class BaseCommandDatabaseContext<TContext> : BaseDatabaseContext<TContext>
    where TContext : DbContext
{
    protected BaseCommandDatabaseContext(DbContextOptions<TContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(BaseCommandDatabaseContext<TContext>).Assembly);
        // Apply all command configurations
        //builder.ApplyConfigurationsFromAssembly(
        //    typeof(BaseCommandDatabaseContext<TContext>).Assembly,
        //    type => type.Namespace?.Contains("Command.Configurations") == true);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Add domain event dispatch logic here if needed
        return base.SaveChangesAsync(cancellationToken);
    }
}
