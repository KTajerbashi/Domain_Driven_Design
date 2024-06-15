using DDD.Core.Domain.Library.Events;

namespace DDD.Core.Domain.Library.Entities;

public interface IAggregateRoot
{
    /// <summary>
    /// رخداد های موجودیت را حذف میکند 
    /// </summary>
    void ClearEvents();
    /// <summary>
    /// تمام رویداد های که روی موجودیت اتفاق می افتد را نگه میدارد
    /// </summary>
    /// <returns></returns>
    IEnumerable<IDomainEvent> GetEvents();
}
