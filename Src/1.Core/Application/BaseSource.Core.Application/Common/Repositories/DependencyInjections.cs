namespace BaseSource.Core.Application.Common.Repositories;

public static class DependencyInjections
{
    /// <summary>
    /// Scrutor
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly[] assemblies)
    {
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes =>
                classes.Where(type => type.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(ICommandRepository<,>))))
            .AddClasses(classes =>
                classes.Where(type => type.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IQueryRepository<,>))))

            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // Also register the open generic fallback if needed
        //services.AddScoped(typeof(ICommandRepository<,>), typeof(CommandRepository<,>));

        return services;
    }

}
