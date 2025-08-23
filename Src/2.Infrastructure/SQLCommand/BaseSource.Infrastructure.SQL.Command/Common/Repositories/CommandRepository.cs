namespace BaseSource.Infrastructure.SQL.Command.Common.Repositories;

public abstract class CommandRepository<TEntity, TId, TContext> : ICommandRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
    where TContext : BaseCommandDatabaseContext<TContext>
{
    private readonly TContext _context;
    protected DbSet<TEntity> Entity { get; }
    private IDbContextTransaction? _transaction;
    protected CommandRepository(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        Entity = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellation = default)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await Entity.AddAsync(entity, cancellation);
    }

    public void BeginTransaction()
        => _context.Database.BeginTransaction();

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => await _context.Database.BeginTransactionAsync(cancellationToken);


    public void CommitTransaction()
        => _context.Database.CommitTransaction();

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        => await _context.Database.BeginTransactionAsync(cancellationToken);


    public async Task DeleteAsync(TId id, CancellationToken cancellation = default)
    {
        var entity = await GetAsync(id, cancellation);
        if (entity == null)
        {
            throw new InfrastructureCommandException($"Record with Id: {id} not found");
        }

        entity.Delete();
        Entity.Update(entity);
    }

    public async Task DeleteAsync(EntityId entityId, CancellationToken cancellation = default)
    {
        if (entityId == null)
            throw new ArgumentNullException(nameof(entityId));

        var entity = await GetAsync(entityId, cancellation);
        if (entity == null)
        {
            throw new InfrastructureCommandException($"Record with EntityId: {entityId} not found");
        }

        entity.Delete();
        Entity.Update(entity);
    }

    public async Task DeleteGraphAsync(EntityId entityId, CancellationToken cancellation = default)
    {
        if (entityId == null)
            throw new ArgumentNullException(nameof(entityId));

        var entity = await GetGraphAsync(entityId, cancellation);
        if (entity == null)
        {
            throw new InfrastructureCommandException($"Record with EntityId: {entityId} not found");
        }

        Entity.Remove(entity);
    }
    public async Task DeleteGraphAsync(TId id, CancellationToken cancellation = default)
    {
        var entity = await GetGraphAsync(id, cancellation);
        if (entity == null)
        {
            throw new InfrastructureCommandException($"Record with Id: {id} not found");
        }

        Entity.Remove(entity);
    }

    public void Dispose()
    {
        DisposeTransaction();
        _context.Dispose();
    }
    private void DisposeTransaction()
    {
        _transaction?.Dispose();
        _transaction = null;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        return await Entity.AnyAsync(expression);
    }

    public async Task<TEntity> GetAsync(TId id, CancellationToken cancellation = default)
    {
        return await Entity.FirstOrDefaultAsync(item => item.Id.Equals(id), cancellation);
    }

    public async Task<TEntity> GetAsync(EntityId entityId, CancellationToken cancellation = default)
    {
        if (entityId == null)
            throw new ArgumentNullException(nameof(entityId));

        return await Entity.FirstOrDefaultAsync(item => item.EntityId.Equals(entityId), cancellation);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellation = default)
    {
        return await Entity.ToListAsync(cancellation);
    }

    public async Task<TEntity> GetGraphAsync(TId id, CancellationToken cancellation = default)
    {
        var query = Entity.AsQueryable();

        // Include all navigation properties
        foreach (var property in _context.Model.FindEntityType(typeof(TEntity)).GetNavigations())
        {
            query = query.Include(property.Name);
        }

        return await query.FirstOrDefaultAsync(item => item.Id.Equals(id), cancellation);
    }

    public async Task<TEntity> GetGraphAsync(EntityId entityId, CancellationToken cancellation = default)
    {
        if (entityId == null)
            throw new ArgumentNullException(nameof(entityId));

        var query = Entity.AsQueryable();

        // Include all navigation properties
        foreach (var property in _context.Model.FindEntityType(typeof(TEntity)).GetNavigations())
        {
            query = query.Include(property.Name);
        }

        return await query.FirstOrDefaultAsync(item => item.EntityId.Equals(entityId), cancellation);
    }

    private TId IncrementId(TId id)
    {
        // This is a simplified approach - you might need a more robust solution
        // based on your specific ID type requirements
        dynamic dynamicId = id;
        return (TId)(dynamicId + 1);
    }
    public async Task<TId> GetNextIdAsync(CancellationToken cancellation = default)
    {
        // Implementation depends on your ID generation strategy
        // This is a simple example assuming sequential IDs
        var maxId = await Entity.MaxAsync(x => x.Id, cancellation);
        return IncrementId(maxId);
    }

    public void RollbackTransaction()
        => _context.Database.RollbackTransaction();

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        => await _context.Database.CommitTransactionAsync(cancellationToken);

    public async Task TransactionAsync(Func<Task> func, CancellationToken cancellationToken = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await func.Invoke(); // execute your business logic
                await SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    public async Task<TResult> TransactionAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await func.Invoke(); // get your return result
                await SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public IEnumerable<TEntity> Queryable(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation = default)
    {
       return Entity.Where(expression).ToList();
    }

    public async Task<IEnumerable<TEntity>> QueryableAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation = default)
    {
       return await Entity.Where(expression).ToListAsync(cancellation);
    }
    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    public async ValueTask DisposeAsync()
    {
        await DisposeTransactionAsync();
        await _context.DisposeAsync();
    }
}
