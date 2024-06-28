using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.ApplicationServices.Library.Events;
using DDD.Core.ApplicationServices.Library.Queries;
using DDD.Core.Contracts.Library.ApplicationServices.Events;
using FluentValidation;

namespace DDD.EndPoints.Web.Library.Extensions.DependencyInjection;

/// <summary>
/// درین اکستنشن تمامی سرویس ها و ریپازیتوری ها و وابستگی هارا که در لایه های 
/// Application 
/// Contract
/// داریم را تزریق میکنیم
/// </summary>
public static class AddApplicationServicesExtensions
{
    /// <summary>
    /// تزریق 
    /// هندلر و دیسپچر های که برای الگوی 
    /// Mediate R
    /// پیاده سازی شده است
    /// Commands    Query  Dispatcher   Event
    /// همچنان تزریق اعتبار سنجی های که برای درخواست نوشته شده است
    /// =========================
    /// بعد از مرحله چهارم اجرا میشود
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                                                                 IEnumerable<Assembly> assembliesForSearch)
        => services.AddCommandHandlers(assembliesForSearch)
                   .AddCommandDispatcherDecorators()
                   .AddQueryHandlers(assembliesForSearch)
                   .AddQueryDispatcherDecorators()
                   .AddEventHandlers(assembliesForSearch)
                   .AddEventDispatcherDecorators()
                   .AddFluentValidators(assembliesForSearch);

    /// <summary>
    /// تزریق تمام هندلر های که از 
    /// ICommandHandler
    /// پیاده سازی یا ارث بری کرده است
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    private static IServiceCollection AddCommandHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandHandler<>), typeof(ICommandHandler<,>));

    /// <summary>
    /// تزریق تمام دیسپچر های که از 
    /// ICommandDispatcher
    /// پیاده سازی یا ارث بری کرده است
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddCommandDispatcherDecorators(this IServiceCollection services)
    {
        services.AddTransient<CommandDispatcher, CommandDispatcher>();
        services.AddTransient<CommandDispatcherDecorator, CommandDispatcherDomainExceptionHandlerDecorator>();
        services.AddTransient<CommandDispatcherDecorator, CommandDispatcherValidationDecorator>();

        ///پیاده سازی پایپ لاین نحوه دیسپچ کردن به ترتیب 
        services.AddTransient<ICommandDispatcher>(c =>
        {
            var commandDispatcher = c.GetRequiredService<CommandDispatcher>();
            var decorators = c.GetServices<CommandDispatcherDecorator>().ToList();
            if (decorators?.Any() == true)
            {
                decorators = decorators.OrderBy(c => c.Order).ToList();
                var listFinalIndex = decorators.Count - 1;
                for (int i = 0; i <= listFinalIndex; i++)
                {
                    if (i == listFinalIndex)
                        decorators[i].SetCommandDispatcher(commandDispatcher);
                    else
                        decorators[i].SetCommandDispatcher(decorators[i + 1]);
                }
                return decorators[0];
            }
            return commandDispatcher;
        });
        return services;
    }

    /// <summary>
    /// تزریق تمام هندلر های که از 
    /// IQueryHandler
    /// پیاده سازی یا ارث بری کرده است
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    private static IServiceCollection AddQueryHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddWithTransientLifetime(assembliesForSearch, typeof(IQueryHandler<,>));

    /// <summary>
    /// تزریق تمام دیسپچر های که از 
    /// IQueryDispatcher
    /// پیاده سازی یا ارث بری کرده است
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddQueryDispatcherDecorators(this IServiceCollection services)
    {
        services.AddTransient<QueryDispatcher, QueryDispatcher>();
        services.AddTransient<QueryDispatcherDecorator, QueryDispatcherDomainExceptionHandlerDecorator>();
        services.AddTransient<QueryDispatcherDecorator, QueryDispatcherValidationDecorator>();

        ///پیاده سازی پایپ لاین نحوه دیسپچ کردن به ترتیب 
        services.AddTransient<IQueryDispatcher>(c =>
        {
            var queryDispatcher = c.GetRequiredService<QueryDispatcher>();
            var decorators = c.GetServices<QueryDispatcherDecorator>().ToList();
            if (decorators?.Any() == true)
            {
                decorators = decorators.OrderBy(c => c.Order).ToList();
                var listFinalIndex = decorators.Count - 1;
                for (int i = 0; i <= listFinalIndex; i++)
                {
                    if (i == listFinalIndex)
                    {
                        decorators[i].SetQueryDispatcher(queryDispatcher);

                    }
                    else
                    {
                        decorators[i].SetQueryDispatcher(decorators[i + 1]);
                    }
                }
                return decorators[0];
            }
            return queryDispatcher;
        });
        return services;
    }

    /// <summary>
    /// تزریق تمام هندلر های که از 
    /// IDomainEventHandler
    /// پیاده سازی یا ارث بری کرده است
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    private static IServiceCollection AddEventHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddWithTransientLifetime(assembliesForSearch, typeof(IDomainEventHandler<>));

    /// <summary>
    /// تزریق تمام دیسپچر های که از 
    /// IEventDispatcher
    /// پیاده سازی یا ارث بری کرده است
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddEventDispatcherDecorators(this IServiceCollection services)
    {
        services.AddTransient<EventDispatcher, EventDispatcher>();
        services.AddTransient<EventDispatcherDecorator, EventDispatcherDomainExceptionHandlerDecorator>();
        services.AddTransient<EventDispatcherDecorator, EventDispatcherValidationDecorator>();

        ///پیاده سازی پایپ لاین نحوه دیسپچ کردن به ترتیب 
        services.AddTransient<IEventDispatcher>(c =>
        {
            var queryDispatcher = c.GetRequiredService<EventDispatcher>();
            var decorators = c.GetServices<EventDispatcherDecorator>().ToList();
            if (decorators?.Any() == true)
            {
                decorators = decorators.OrderBy(c => c.Order).ToList();
                var listFinalIndex = decorators.Count - 1;
                for (int i = 0; i <= listFinalIndex; i++)
                {
                    if (i == listFinalIndex)
                    {
                        decorators[i].SetEventDispatcher(queryDispatcher);

                    }
                    else
                    {
                        decorators[i].SetEventDispatcher(decorators[i + 1]);
                    }
                }
                return decorators[0];
            }
            return queryDispatcher;
        });
        return services;
    }

    /// <summary>
    /// تزریق 
    /// Fluent Validations
    /// برای درخواست های که نوشته شده است
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    private static IServiceCollection AddFluentValidators(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddValidatorsFromAssemblies(assembliesForSearch);

}

