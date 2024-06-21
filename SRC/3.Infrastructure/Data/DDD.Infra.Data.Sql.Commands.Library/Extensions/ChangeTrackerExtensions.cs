using DDD.Core.Domain.Library.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DDD.Infra.Data.Sql.Commands.Library.Extensions;
/// <summary>
/// 
/// </summary>
public static class ChangeTrackerExtensions
{
    /// <summary>
    /// ترکر های بده که 
    /// Aggregate Root  
    /// هستند
    /// </summary>
    /// <param name="changeTracker"></param>
    /// <returns></returns>
    public static List<AggregateRoot> GetChangedAggregates(this ChangeTracker changeTracker) =>
        changeTracker.Aggregates().Where(IsModified()).Select(c => c.Entity).ToList();

    /// <summary>
    /// ترکر های که رویداد درون خود دارند
    /// </summary>
    /// <param name="changeTracker"></param>
    /// <returns></returns>
    public static List<AggregateRoot> GetAggregatesWithEvent(this ChangeTracker changeTracker) =>
        changeTracker
        .Aggregates()
        .Where(IsNotDetached()).Select(c => c.Entity).Where(c => c.GetEvents().Any()).ToList();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="changeTracker"></param>
    /// <returns></returns>
    public static IEnumerable<EntityEntry<AggregateRoot>> Aggregates(this ChangeTracker changeTracker) =>
        changeTracker.Entries<AggregateRoot>();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static Func<EntityEntry<AggregateRoot>, bool> IsNotDetached() =>
        x => x.State != EntityState.Detached;


    /// <summary>
    /// این شاخه های که تغییرات داشته را بر میگرداند
    /// </summary>
    /// <returns></returns>
    private static Func<EntityEntry<AggregateRoot>, bool> IsModified()
    {
        return x => x.State == EntityState.Modified ||
                                           x.State == EntityState.Added ||
                                           x.State == EntityState.Deleted;
    }

}
