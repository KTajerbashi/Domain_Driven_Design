using DDD.Core.Contracts.Library.Data.Commands;
using DDD.Core.Contracts.Library.Data.Queries;

namespace DDD.EndPoints.Web.Library.Extensions.DependencyInjection;
/// <summary>
/// توابع کمکی جهت ثبت نیازمندی‌های لایه داده
/// </summary>
public static class AddDataAccessExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) =>
        services.AddRepositories(assembliesForSearch).AddUnitOfWorks(assembliesForSearch);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) =>
        services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandRepository<,>), typeof(IQueryRepository));
   
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnitOfWorks(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) =>
        services.AddWithTransientLifetime(assembliesForSearch, typeof(IUnitOfWork));
}
