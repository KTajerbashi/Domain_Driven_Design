using Extensions.DependencyInjection.Abstractions;
using Extensions.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace Extensions.DependencyInjection.Extensions.DependencyInjection;

public static class DependencyInjectionServiceCollectionExtensions
{
    public static IServiceCollection AddCustomDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var option = configuration.Get<DependencyInjectionOption>();

        services.AddWithTransientLifetime(option.AssemblyNamesForLoad)
            .AddWithScopeLifetime(option.AssemblyNamesForLoad)
            .AddWithSingletonLifetime(option.AssemblyNamesForLoad)
            .Configure<DependencyInjectionOption>(configuration);

        return services;
    }

    public static IServiceCollection AddCustomDependencies(this IServiceCollection services, IConfiguration configuration, string sectionName)
        => services.AddCustomDependencies(configuration.GetSection(sectionName));

    public static IServiceCollection AddCustomDependencies(this IServiceCollection services, Action<DependencyInjectionOption> setupAction)
    {
        var option = new DependencyInjectionOption();
        setupAction.Invoke(option);

        services.AddWithTransientLifetime(option.AssemblyNamesForLoad)
            .AddWithScopeLifetime(option.AssemblyNamesForLoad)
            .AddWithSingletonLifetime(option.AssemblyNamesForLoad)
            .Configure(setupAction);

        return services;
    }

    private static IServiceCollection AddWithTransientLifetime(this IServiceCollection services, string assemblyNames)
        => services.Scan(s => s.FromAssemblies(GetAssemblies(assemblyNames))
        .AddClasses(c => c.AssignableToAny(typeof(ITransientLifetime)))
        .AsImplementedInterfaces()
        .WithTransientLifetime());

    private static IServiceCollection AddWithScopeLifetime(this IServiceCollection services, string assemblyNames)
        => services.Scan(s => s.FromAssemblies(GetAssemblies(assemblyNames))
        .AddClasses(c => c.AssignableToAny(typeof(IScopeLifetime)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

    private static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services, string assemblyNames)
        => services.Scan(s => s.FromAssemblies(GetAssemblies(assemblyNames))
        .AddClasses(c => c.AssignableToAny(typeof(ISingletoneLifetime)))
        .AsImplementedInterfaces()
        .WithSingletonLifetime());

    private static List<Assembly> GetAssemblies(string assemblyNames)
    {
        var assemblies = new List<Assembly>();
        var dependencies = DependencyContext.Default.RuntimeLibraries;

        foreach (var library in dependencies)
        {
            if (IsCandidateCompilationLibrary(library, assemblyNames.Split(',')))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
        }

        return assemblies;
    }

    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assemblyName)
        => assemblyName.Any(d => compilationLibrary.Name.Contains(d)) || compilationLibrary.Dependencies.Any(d => assemblyName.Any(c => d.Name.Contains(c)));
}
