namespace BaseSource.Core.Application.Common.Patterns;

/// <summary>
/// تعریف Interface برای الگوی UnitOfWork جهت مدیریت تراکنش‌ها با دیتابیس در این قسمت انجام شده است
/// تعریف کامل این الگو در کتاب P of EAA وجود دارد و تعریف اولیه را در آدرس زیر می‌توان مشاهده کرد
/// https://martinfowler.com/eaaCatalog/unitOfWork.html
/// </summary>
public interface IUnitOfWork : IDisposable, IAsyncDisposable, IScopedLifetime
{

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    void BeginTransaction();
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);


    void CommitTransaction();
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);


    void RollbackTransaction();
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    Task TransactionAsync(Func<Task> func, CancellationToken cancellationToken = default);
    Task<TResult> TransactionAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken = default);
}

