using DDD.Core.Contracts.Library.Data.Queries;

namespace DDD.Core.Data.Sql.Queries.Library;
/// <summary>
/// 
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public class BaseQueryRepository<TDbContext> : IQueryRepository
    where TDbContext : BaseQueryDbContext
{
    protected readonly TDbContext _dbContext;
    public BaseQueryRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
