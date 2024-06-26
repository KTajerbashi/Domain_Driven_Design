﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Extensions.ObjectMappers.Abstractions;
using Extensions.ObjectMappers.AutoMapper.Options;
using Extensions.ObjectMappers.AutoMapper.Services;
using Extensions.ObjectMappers.AutoMapper.Extensions.DependencyInjection;

namespace Extensions.ObjectMappers.AutoMapper.Extensions.DependencyInjection;

/// <summary>
/// در ادامه مرحله اجرا میشود
/// </summary>
public static class AutoMapperServiceCollectionExtensions
{
    /// <summary>
    /// 
    /// ======================================
    /// در ادامه مرحله چهارم اجرا میشود
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="sectionName"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services,
                                                          IConfiguration configuration,
                                                          string sectionName)
        => services.AddAutoMapperProfiles(configuration.GetSection(sectionName));

    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, IConfiguration configuration)
    {
        var option = configuration.Get<AutoMapperOption>();

        var assemblies = GetAssemblies(option.AssemblyNamesForLoadProfiles);

        return services.AddAutoMapper(assemblies).AddSingleton<IMapperAdapter, AutoMapperAdapter>();
    }

    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, Action<AutoMapperOption> setupAction)
    {
        var option = new AutoMapperOption();
        setupAction.Invoke(option);

        var assemblies = GetAssemblies(option.AssemblyNamesForLoadProfiles);

        return services.AddAutoMapper(assemblies).AddSingleton<IMapperAdapter, AutoMapperAdapter>();
    }

    /// <summary>
    /// در ادامه مرحله چهارم
    /// </summary>
    /// <param name="assemblyNames"></param>
    /// <returns></returns>
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
        => assemblyName.Any(d => compilationLibrary.Name.Contains(d))
           || compilationLibrary.Dependencies.Any(d => assemblyName.Any(c => d.Name.Contains(c)));
}