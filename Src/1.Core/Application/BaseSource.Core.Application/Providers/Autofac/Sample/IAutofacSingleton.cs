namespace BaseSource.Core.Application.Providers.Autofac.Sample;

public interface IAutofacSingleton
{
    Guid Execute();
}

// Singleton service example
public class AutofacSingleton : IAutofacSingleton, IAutofacSingletonLifetime
{
    Guid Guid;
    public AutofacSingleton()
    {
        Guid = Guid.NewGuid();
    }
    public Guid Execute()
    {
        return Guid; // Return a new Guid for each email sent
    }
}

public interface IAutofacScoped
{
    Guid Execute();
}

// Scoped service example
public class AutofacScoped : IAutofacScoped, IAutofacScopedLifetime
{
    Guid Guid;
    public AutofacScoped()
    {
        Guid = Guid.NewGuid();
    }
    public Guid Execute()
    {
        return Guid; // Return a new Guid for each email sent
    }
}

public interface IAutofacTransient
{
    Guid Execute();
}

// Transient service example
public class AutofacTransient : IAutofacTransient, IAutofacTransientLifetime
{
    Guid Guid;
    public AutofacTransient()
    {
        Guid = Guid.NewGuid();
    }
    public Guid Execute()
    {
        return Guid; // Return a new Guid for each email sent
    }
}