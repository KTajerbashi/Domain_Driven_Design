
using DDD.Core.Contracts.Library.Data.Commands;

namespace DDD.Infra.Data.Sql.Commands.Library;
/// <summary>
/// پیاده سازی الگوی
/// Unit Of Work
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public abstract class BaseEntityFrameworkUnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : BaseCommandDbContext
{
    protected readonly TDbContext _dbContext;

    public BaseEntityFrameworkUnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void BeginTransaction()
    {
        _dbContext.BeginTransaction();
    }

    public int Commit()
        => _dbContext.SaveChanges();

    public async Task<int> CommitAsync()
        => await _dbContext.SaveChangesAsync();

    public void CommitTransaction()
    {
        _dbContext.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        _dbContext.RollbackTransaction();
    }
}

