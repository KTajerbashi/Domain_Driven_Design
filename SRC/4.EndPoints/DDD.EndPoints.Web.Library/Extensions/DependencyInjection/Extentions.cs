using Extensions.DependencyInjection.Abstractions;

namespace DDD.EndPoints.Web.Library.Extensions.DependencyInjection;

/// <summary>
/// 
/// </summary>
public static class Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblyNamesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddDependencies(this IServiceCollection services,
        params string[] assemblyNamesForSearch)
    {

        var assemblies = GetAssemblies(assemblyNamesForSearch);
        services
            .AddZaminApplicationServices(assemblies)
            .AddZaminDataAccess(assemblies)
            .AddZaminUntilityServices()
            .AddCustomeDepenecies(assemblies);
        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomeDepenecies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        return services.AddWithTransientLifetime(assemblies, typeof(ITransientLifetime))
            .AddWithScopedLifetime(assemblies, typeof(IScopeLifetime))
            .AddWithSingletonLifetime(assemblies, typeof(ISingletoneLifetime));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <param name="assignableTo"></param>
    /// <returns></returns>
    public static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        return services;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <param name="assignableTo"></param>
    /// <returns></returns>
    public static IServiceCollection AddWithScopedLifetime(this IServiceCollection services,
       IEnumerable<Assembly> assembliesForSearch,
       params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <param name="assignableTo"></param>
    /// <returns></returns>
    public static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
        return services;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="assmblyName"></param>
    /// <returns></returns>
    private static List<Assembly> GetAssemblies(string[] assmblyName)
    {

        var assemblies = new List<Assembly>();
        var dependencies = DependencyContext.Default.RuntimeLibraries;
        foreach (var library in dependencies)
        {
            if (IsCandidateCompilationLibrary(library, assmblyName))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
        }
        return assemblies;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="compilationLibrary"></param>
    /// <param name="assmblyName"></param>
    /// <returns></returns>
    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assmblyName)
    {
        return assmblyName.Any(d => compilationLibrary.Name.Contains(d))
            || compilationLibrary.Dependencies.Any(d => assmblyName.Any(c => d.Name.Contains(c)));
    }

}