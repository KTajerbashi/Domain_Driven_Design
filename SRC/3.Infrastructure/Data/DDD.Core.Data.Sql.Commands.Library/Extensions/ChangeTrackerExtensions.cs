using DDD.Core.Domain.Library.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DDD.Core.Data.Sql.Commands.Library.Extensions;
/// <summary>
/// 
/// </summary>
public static class ChangeTrackerExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="changeTracker"></param>
    /// <returns></returns>
    public static List<AggregateRoot> GetChangedAggregates(this ChangeTracker changeTracker) =>
        changeTracker.Aggreates().Where(IsModified()).Select(c => c.Entity).ToList();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="changeTracker"></param>
    /// <returns></returns>
    public static List<AggregateRoot> GetAggregatesWithEvent(this ChangeTracker changeTracker) =>
            changeTracker.Aggreates()
                                     .Where(IsNotDetached()).Select(c => c.Entity).Where(c => c.GetEvents().Any()).ToList();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="changeTracker"></param>
    /// <returns></returns>
    public static IEnumerable<EntityEntry<AggregateRoot>> Aggreates(this ChangeTracker changeTracker) =>
        changeTracker.Entries<AggregateRoot>();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static Func<EntityEntry<AggregateRoot>, bool> IsNotDetached() =>
        x => x.State != EntityState.Detached;
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static Func<EntityEntry<AggregateRoot>, bool> IsModified()
    {
        return x => x.State == EntityState.Modified ||
                                           x.State == EntityState.Added ||
                                           x.State == EntityState.Deleted;
    }

}
