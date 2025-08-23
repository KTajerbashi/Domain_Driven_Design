using BaseSource.Core.Domain.Common.Aggregate;
using BaseSource.Core.Domain.Common.ValueObjects;
using System.Linq.Expressions;

namespace BaseSource.Core.Application.Common.Repositories;

public interface IQueryRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{
    Task<TEntity> GetAsync(TId id, CancellationToken cancellation = default);
    Task<TEntity> GetGraphAsync(TId id, CancellationToken cancellation = default);
    Task<TEntity> GetGraphAsync(EntityId entityId, CancellationToken cancellation = default);
    Task<TEntity> GetAsync(EntityId entityId, CancellationToken cancellation = default);
    Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellation = default);
    Task<IEnumerable<TEntity>> QueryableAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation = default);
    IEnumerable<TEntity> Queryable(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation = default);
}

