using BaseSource.Infrastructure.SQL.Common.Persistence;

namespace BaseSource.Infrastructure.SQL.Command.Common.Persistence;

public abstract class BaseCommandDatabaseContext<TContext> : BaseDatabaseContext<TContext>
    where TContext : DbContext
{
    protected BaseCommandDatabaseContext(DbContextOptions<TContext> options) : base(options)
    {
    }
}
