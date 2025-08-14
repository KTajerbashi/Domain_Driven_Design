using BaseSource.Core.Application.Common.Repositories;
using BaseSource.Core.Domain.Common.Aggregate;
using BaseSource.Core.Domain.Common.ValueObjects;
using BaseSource.Infrastructure.SQL.Query.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace BaseSource.Infrastructure.SQL.Query.Common.Repositories;

public class QueryRepository<TEntity, TId, TContext> : IQueryRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
    where TContext : BaseQueryDatabaseContext<TContext>
{

    private readonly TContext _context;
    protected DbSet<TEntity> Entity { get; }
    protected QueryRepository(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        Entity = context.Set<TEntity>();
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


    public IEnumerable<TEntity> Queryable(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation = default)
    {
        return Entity.Where(expression).ToList();
    }

    public async Task<IEnumerable<TEntity>> QueryableAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation = default)
    {
        return await Entity.Where(expression).ToListAsync(cancellation);
    }
}
