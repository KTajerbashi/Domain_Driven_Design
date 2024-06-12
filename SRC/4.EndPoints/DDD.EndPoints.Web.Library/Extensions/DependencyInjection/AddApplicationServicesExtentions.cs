using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.ApplicationServices.Library.Events;
using DDD.Core.ApplicationServices.Library.Queries;
using DDD.Core.Contracts.Library.ApplicationServices.Commands;
using DDD.Core.Contracts.Library.ApplicationServices.Events;
using DDD.Core.Contracts.Library.ApplicationServices.Queries;
using FluentValidation;

namespace DDD.EndPoints.Web.Library.Extensions.DependencyInjection;

/// <summary>
/// 
/// </summary>
public static class AddApplicationServicesExtensions
{
    /// <summary>
    /// 
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
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    private static IServiceCollection AddCommandHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandHandler<>), typeof(ICommandHandler<,>));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddCommandDispatcherDecorators(this IServiceCollection services)
    {
        services.AddTransient<CommandDispatcher, CommandDispatcher>();
        services.AddTransient<CommandDispatcherDecorator, CommandDispatcherDomainExceptionHandlerDecorator>();
        services.AddTransient<CommandDispatcherDecorator, CommandDispatcherValidationDecorator>();

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
                    {
                        decorators[i].SetCommandDispatcher(commandDispatcher);

                    }
                    else
                    {
                        decorators[i].SetCommandDispatcher(decorators[i + 1]);
                    }
                }
                return decorators[0];
            }
            return commandDispatcher;
        });
        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    private static IServiceCollection AddQueryHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddWithTransientLifetime(assembliesForSearch, typeof(IQueryHandler<,>));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddQueryDispatcherDecorators(this IServiceCollection services)
    {
        services.AddTransient<QueryDispatcher, QueryDispatcher>();
        services.AddTransient<QueryDispatcherDecorator, QueryDispatcherDomainExceptionHandlerDecorator>();
        services.AddTransient<QueryDispatcherDecorator, QueryDispatcherValidationDecorator>();

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
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    private static IServiceCollection AddEventHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddWithTransientLifetime(assembliesForSearch, typeof(IDomainEventHandler<>));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddEventDispatcherDecorators(this IServiceCollection services)
    {
        services.AddTransient<EventDispatcher, EventDispatcher>();
        services.AddTransient<EventDispatcherDecorator, EventDispatcherDomainExceptionHandlerDecorator>();
        services.AddTransient<EventDispatcherDecorator, EventDispatcherValidationDecorator>();

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
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    private static IServiceCollection AddFluentValidators(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddValidatorsFromAssemblies(assembliesForSearch);
}

