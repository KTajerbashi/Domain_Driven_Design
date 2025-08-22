namespace BaseSource.Core.Domain.Common.ValueObjects;

/// <summary>
/// کلاس پایه برای همه ValueObjectها
/// توضحیات کاملی در مورد دلایل وجود ValueObjectها را در لینک زیر مشاهده می‌کند
/// https://martinfowler.com/bliki/ValueObject.html
/// </summary>
/// <typeparam name="TValueObject"></typeparam>
public abstract class BaseValueObject<TValueObject> : IEquatable<TValueObject>
        where TValueObject : BaseValueObject<TValueObject>
{
    /// <summary>
    /// Check A Value Object is Equal a seperate ValueObject
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(TValueObject other) => this == other;

    /// <summary>
    /// Override the base method to Check Equality
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is TValueObject otherObject)
        {
            return GetEqualityComponents().SequenceEqual(otherObject.GetEqualityComponents());
        }
        return false;
    }
    
    /// <summary>
    /// Override the base method to manage hashcode of object
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y); // XOR is a common, simple combining algorithm
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<object> GetEqualityComponents();
    public static bool operator ==(BaseValueObject<TValueObject> right, BaseValueObject<TValueObject> left)
    {
        if (right is null && left is null)
            return true;
        if (right is null || left is null)
            return false;
        return right.Equals(left);
    }
    public static bool operator !=(BaseValueObject<TValueObject> right, BaseValueObject<TValueObject> left) => !(right == left);


}

