namespace BaseSource.Core.Domain.Common.Patterns.IDsTyped;

// Base interface for strongly-typed IDs
public interface IStronglyTypedId<T> where T : notnull
{
    T Value { get; }
}

// Base implementation for strongly-typed IDs
public abstract record StronglyTypedId<T>(T Value) : IStronglyTypedId<T>
    where T : notnull
{
    public override string ToString() => Value.ToString() ?? string.Empty;
}

// Example implementation
public record ProductId(Guid Value) : StronglyTypedId<Guid>(Value);
