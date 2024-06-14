using Extensions.ChangeDataLog.Abstractions;
using Extensions.ChangeDataLog.Sql.Options;
using Extensions.UsersManagement.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;

namespace Infra.Data.Sql.Commands.Interceptors;

/// <summary>
/// کلاس اضافه کردن تغییرات روی موجودیت در پایگاه داده
/// </summary>
public class AddChangeDataLogInterceptor : SaveChangesInterceptor
{
    /// <summary>
    /// ذخیره کردن لاگ تغییرات که روی موجودیت انجام شده است
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SaveEntityChangeLogs(eventData);
        return base.SavingChanges(eventData, result);
    }

    /// <summary>
    /// ذخیره کردن لاگ تغییرات که روی موجودیت انجام شده است بصورت اسینک
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        SaveEntityChangeLogs(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// پیاده سازی متد ذخیره کردن لاگ تغییرات روی موجودیت انجام شده همراه با
    /// ChangeTracker
    /// </summary>
    /// <param name="eventData"></param>
    private void SaveEntityChangeLogs(DbContextEventData eventData)
    {
        var changeTracker = eventData.Context.ChangeTracker;
        var userInfoService = eventData.Context.GetService<IUserInfoService>();
        var itemRepository = eventData.Context.GetService<IEntityChangeInterceptorItemRepository>();
        var options = eventData.Context.GetService<IOptions<ChangeDataLogHamsterOptions>>().Value;
        var changedEntities = GetChangedEntities(changeTracker);
        var transactionId = Guid.NewGuid().ToString();
        var dateOfAccured = DateTime.Now;

        var entityChageInterceptorItems = new List<EntityChangeInterceptorItem>();

        foreach (var entity in changedEntities)
        {
            var entityChageInterceptorItem = new EntityChangeInterceptorItem
            {
                Id = Guid.NewGuid(),
                TransactionId = transactionId,
                UserId = userInfoService.UserId().ToString(),
                Ip = userInfoService.GetUserIp(),
                EntityType = entity.Entity.GetType().FullName,
                EntityId = entity.Property(options.BusinessIdFieldName).CurrentValue.ToString(),
                DateOfOccurrence = dateOfAccured,
                ChangeType = entity.State.ToString(),
                ContextName = GetType().FullName
            };

            foreach (var property in entity.Properties.Where(c => options.propertyForReject.All(d => d != c.Metadata.Name)))
            {
                if (entity.State == EntityState.Added || property.IsModified)
                {
                    entityChageInterceptorItem.PropertyChangeLogItems.Add(new PropertyChangeLogItem
                    {
                        ChageInterceptorItemId = entityChageInterceptorItem.Id,
                        PropertyName = property.Metadata.Name,
                        Value = property.CurrentValue?.ToString(),
                    });
                }
            }
            entityChageInterceptorItems.Add(entityChageInterceptorItem);
        }
        itemRepository.Save(entityChageInterceptorItems);


    }

    /// <summary>
    /// دریافت لیست تغییرات روی موجودیت
    /// </summary>
    /// <param name="changeTracker"></param>
    /// <returns></returns>
    private List<EntityEntry> GetChangedEntities(ChangeTracker changeTracker) =>
       changeTracker.Entries()
           .Where(x => x.State == EntityState.Modified
           || x.State == EntityState.Added
           || x.State == EntityState.Deleted).ToList();
}
