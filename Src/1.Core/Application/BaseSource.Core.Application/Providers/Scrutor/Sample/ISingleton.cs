using BaseSource.Core.Application.Providers.Scrutor;

namespace BaseSource.Core.Application.Providers.Scrutor.Sample;
public interface ISingleton
{
    Guid Execute();
}

// Singleton service example
public class Singleton : ISingleton, ISingletonLifetime
{
    Guid Guid;
    public Singleton()
    {
        Guid = Guid.NewGuid();
    }
    public Guid Execute()
    {
        return Guid; // Return a new Guid for each email sent
    }
}

public interface IScoped
{
    Guid Execute();
}

// Scoped service example
public class Scoped : IScoped, IScopedLifetime
{
    Guid Guid;
    public Scoped()
    {
        Guid = Guid.NewGuid();
    }
    public Guid Execute()
    {
        return Guid; // Return a new Guid for each email sent
    }
}

public interface ITransient
{
    Guid Execute();
}

// Transient service example
public class Transient : ITransient, ITransientLifetime
{
    Guid Guid;
    public Transient()
    {
        Guid = Guid.NewGuid();
    }
    public Guid Execute()
    {
        return Guid; // Return a new Guid for each email sent
    }
}