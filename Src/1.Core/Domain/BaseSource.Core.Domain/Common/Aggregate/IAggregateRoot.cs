using System.Security.Principal;

namespace BaseSource.Core.Domain.Common.Aggregate;


public interface IEntity<TId>
    where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{
    TId Id { get; }
    EntityId EntityId { get; }
}

public interface IAuditableEntity<TId> : IEntity<TId>
    where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{
    bool IsDeleted { get; }
    bool IsActive{ get; }
    void Delete();
    void Restore();
}

public interface IAggregateRoot<TId> : IAuditableEntity<TId>
    where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{
    void ClearEvents();
    IEnumerable<IDomainEvent> GetEvents();
}

/// <summary>
/// کلاس پایه برای تمامی Entityها موجود در سامانه
/// </summary>

public abstract class Entity<TId> : IAuditableEntity<TId>
    where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{
    /// <summary>
    /// شناسه عددی Entityها
    /// صرفا برای ذخیره در دیتابیس و سادگی کار مورد استفاده قرار بگیرید.
    /// </summary>
    public TId Id { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public bool IsActive { get; protected set; }

    public EntityId EntityId { get; protected set; } = EntityId.FromGuid(Guid.NewGuid());


    protected Entity() { }


    #region Equality Check
    public bool Equals(Entity<TId>? other) => this == other;
    public override bool Equals(object? obj) =>
         obj is Entity<TId> otherObject && Id.Equals(otherObject.Id);

    public override int GetHashCode() => Id.GetHashCode();

    public void Delete()
    {
        IsDeleted = true;
        IsActive = false;
    }

    public void Restore()
    {
        IsDeleted = false;
        IsActive = true;
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals((object)right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
        => !(right == left);

    #endregion
}
public abstract class Entity : Entity<long> { }



/// <summary>
/// پیاده سازی الگوی AggregateRoot
/// توضیحات کامل در مورد این الگو را در آدرس زیر می‌توانید مشاهده نمایید
/// https://martinfowler.com/bliki/DDD_Aggregate.html
/// 
/// </summary>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
    where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{
    /// <summary>
    /// لیست Evantهای مربوطه را نگهداری می‌کند        
    /// </summary>
    private readonly List<IDomainEvent> _events;
    protected AggregateRoot() => _events = new();

    /// <summary>
    /// سازنده Aggregate برای ایجاد Aggregate از روی Eventها
    /// </summary>
    /// <param name="events">در صورتی که Event از قبل وجود داشته باشد توسط این پارامتر به Aggregate ارسال می‌گردد</param>
    public AggregateRoot(IEnumerable<IDomainEvent> events)
    {
        if (events == null || !events.Any()) return;
        foreach (var @event in events)
        {
            Mutate(@event);
        }
    }

    protected void Apply(IDomainEvent @event)
    {
        Mutate(@event);
        AddEvent(@event);
    }

    private void Mutate(IDomainEvent @event)
    {
        var onMethod = this.GetType().GetMethod("On", BindingFlags.Instance | BindingFlags.NonPublic, [@event.GetType()]);
        onMethod.Invoke(this, new[] { @event });
    }

    /// <summary>
    /// یک Event جدید به مجموعه Eventهای موجود در این Aggregate اضافه می‌کند.
    /// مسئولیت ساخت و ارسال Event به عهده خود Aggregateها می‌باشد.
    /// </summary>
    /// <param name="event"></param>
    protected void AddEvent(IDomainEvent @event) => _events.Add(@event);

    /// <summary>
    /// لیستی از Eventهای رخداده برای Aggregate را به صورت فقط خواندی و غیر قابل تغییر را بازگشت می‌دهد
    /// </summary>
    /// <returns>لیست Eventها</returns>
    public IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();

    /// <summary>
    /// Eventهای موجود در این Aggregate را پاک می‌کند
    /// </summary>
    public void ClearEvents() => _events.Clear();
}
public abstract class AggregateRoot : AggregateRoot<long> { }