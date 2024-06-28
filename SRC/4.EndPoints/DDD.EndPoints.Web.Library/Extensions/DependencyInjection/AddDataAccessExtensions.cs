namespace DDD.EndPoints.Web.Library.Extensions.DependencyInjection;
/// <summary>
/// توابع کمکی جهت ثبت نیازمندی‌های لایه داده
/// </summary>
public static class AddDataAccessExtensions
{
    /// <summary>
    /// تزریق کردن تمام ریپازیتوری هایکه که برای سرویس ها پیاده سازی میشوند
    /// و تزریق الگوی 
    /// Unit Of Work
    /// ======================
    /// بعد از مرحله چهارم اجرا میشود
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) => services.AddRepositories(assembliesForSearch)
                                                              .AddUnitOfWorks(assembliesForSearch);

    /// <summary>
    /// تزریق تمام ریپازیتوری هایکه از 
    /// ICommandRepository
    /// ارث بری یا پیاده سازی میکند
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) =>
        services.AddWithTransientLifetime(assembliesForSearch,typeof(ICommandRepository<,>),typeof(IQueryRepository));

    /// <summary>
    /// تزریق تمام ریپازیتوری هایکه از 
    /// IUnitOfWork
    /// ارث بری یا پیاده سازی میکند
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnitOfWorks(
        this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) =>
        services.AddWithTransientLifetime(assembliesForSearch, typeof(IUnitOfWork));
}
