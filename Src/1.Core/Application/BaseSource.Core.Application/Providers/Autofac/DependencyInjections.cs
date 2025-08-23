namespace BaseSource.Core.Application.Providers.Autofac;


public static class DependencyInjections
{
    public static ContainerBuilder AddAutofacLifetimeServices(this ContainerBuilder builder, Assembly[] assemblies)
    {
        // Singleton
        builder.RegisterAssemblyTypes(assemblies)
            .Where(t => typeof(IAutofacSingletonLifetime).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .SingleInstance();

        // Scoped
        builder.RegisterAssemblyTypes(assemblies)
            .Where(t => typeof(IAutofacScopedLifetime).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        // Transient
        builder.RegisterAssemblyTypes(assemblies)
            .Where(t => typeof(IAutofacTransientLifetime).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerDependency();

        return builder;
    }
}


