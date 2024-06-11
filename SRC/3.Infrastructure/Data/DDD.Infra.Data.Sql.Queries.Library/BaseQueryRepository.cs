using DDD.Core.Contracts.Library.Data.Queries;

namespace DDD.Infra.Data.Sql.Queries.Library;
public class BaseQueryRepository<TDbContext> : IQueryRepository
    where TDbContext : BaseQueryDbContext
{
    protected readonly TDbContext _dbContext;
    public BaseQueryRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
