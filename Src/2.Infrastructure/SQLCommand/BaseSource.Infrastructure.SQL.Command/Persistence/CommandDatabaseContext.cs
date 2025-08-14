namespace BaseSource.Infrastructure.SQL.Command.Persistence;

public class CommandDatabaseContext : BaseCommandDatabaseContext
{
    public CommandDatabaseContext()
    {
    }

    public CommandDatabaseContext(DbContextOptions<CommandDatabaseContext> options) : base(options)
    {
    }
}