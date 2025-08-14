using BaseSource.Infrastructure.SQL.Common.Persistence;

namespace BaseSource.Infrastructure.SQL.Command.Common.Persistence;

public abstract class BaseCommandDatabaseContext : BaseDatabaseContext
{
    protected BaseCommandDatabaseContext()
    {
    }

    protected BaseCommandDatabaseContext(DbContextOptions options) : base(options)
    {
    }
}
