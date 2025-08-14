using BaseSource.Infrastructure.SQL.Common.Persistence;
using BaseSource.Infrastructure.SQL.Query.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.Infrastructure.SQL.Query.Common.Persistence;

public abstract class BaseQueryDatabaseContext<TContext> : BaseDatabaseContext<TContext>
    where TContext : DbContext
{
    protected BaseQueryDatabaseContext()
    {
    }

    protected BaseQueryDatabaseContext(DbContextOptions<TContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        throw new InfrastructureQueryException("You Have Not Access To Change !!!");
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InfrastructureQueryException("You Have Not Access To Change !!!");
    }
    public override int SaveChanges()
    {
        throw new InfrastructureQueryException("You Have Not Access To Change !!!");
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InfrastructureQueryException("You Have Not Access To Change !!!");
    }
}
