using Extensions.DependencyInjection.Abstractions;

namespace DDD.EndPoints.Web.Library.Extensions.DependencyInjection;

/// <summary>
/// این اکستنشن کلاس مرکزیت وابستگی ها و تزریق های است
/// </summary>
public static class Extensions
{
    /// <summary>
    /// درین وابستگی های لایه اپلیکشن
    /// لایه دیتابیس
    /// لایه یوتیلیتیس
    /// و بقیه وابستگی ها میباشد
    /// ==========================================
    /// بعد از مرحله چهارم اجرا میشود
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblyNamesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddDependencies(this IServiceCollection services,
        params string[] assemblyNamesForSearch)
    {

        var assemblies = GetAssemblies(assemblyNamesForSearch);
        services
            .AddApplicationServices(assemblies)
            .AddDataAccess(assemblies)
            .AddUtilityServices()
            .AddCustomDependencies(assemblies);
        return services;
    }

    /// <summary>
    /// این اکستنشن اینترفس های لایف تایم را تزریق میکند
    /// =======================
    /// بعد از مرحله چهارم اجرا میشود
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomDependencies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        return services
                        .AddWithTransientLifetime(assemblies, typeof(ITransientLifetime))
                        .AddWithScopedLifetime(assemblies, typeof(IScopeLifetime))
                        .AddWithSingletonLifetime(assemblies, typeof(ISingletoneLifetime));
    }
   
    
    /// <summary>
    /// سرویس های که لایف تایم که از نوعیت ترانزینت باشد
    /// با این تزریق میکنیم
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <param name="assignableTo"></param>
    /// <returns></returns>
    public static IServiceCollection AddWithTransientLifetime(
        this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services
            .Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        return services;
    }

    /// <summary>
    /// سرویس های که لایف تایم که از نوعیت اسکوپ باشد
    /// با این تزریق میکنیم
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
    /// سرویس های که لایف تایم که از نوعیت سینگلتون باشد
    /// با این تزریق میکنیم
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
    /// تمام اسیمبلی هارا براساس نام پروژه را اضافه میکنیم
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <returns></returns>
    private static List<Assembly> GetAssemblies(string[] assemblyName)
    {
        var assemblies = new List<Assembly>();
        var dependencies = DependencyContext.Default.RuntimeLibraries;
        foreach (var library in dependencies)
        {
            if (IsCandidateCompilationLibrary(library, assemblyName))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
        }
        return assemblies;
    }


    /// <summary>   👆👆👆👆👆👆👆👆👆👆👆👆
    /// این سرویس هارا دریافت میکند
    /// که آیا این وابستگی در اسیمبلی مورد نظر است یا خیر
    /// در متد دریافت اسیمبلی
    /// </summary>
    /// <param name="compilationLibrary"></param>
    /// <param name="assemblyName"></param>
    /// <returns></returns>
    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assemblyName)
    {
        return assemblyName.Any(d => compilationLibrary.Name.Contains(d))
            || compilationLibrary.Dependencies.Any(d => assemblyName.Any(c => d.Name.Contains(c)));
    }

}