namespace BaseSource.Core.Application.Providers.MapperObjects;

public static class DependencyInjection
{
    public static IServiceCollection AddAutoMapperProfiles(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly[] assemblies)
    {
        services.AddAutoMapper(options =>
        {
            options.AddMaps(assemblies);
        });

        services.AddSingleton<IMapperAdapter, MapperAdapter>();

        return services;
    }
}