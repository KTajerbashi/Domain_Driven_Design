using System.Reflection;

namespace BaseSource.Core.Application.Providers.Scrutor;

public static class DependencyInjection
{
    public static IServiceCollection AddScrutorProvider(this IServiceCollection services, Assembly[] assemblies)
    {
        // Get the assembly where your services are located

        // Register services with Scrutor based on their lifetime interfaces
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo<ISingletonLifetime>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime()

            .AddClasses(classes => classes.AssignableTo<IScopedLifetime>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo<ITransientLifetime>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        return services;
    }
}