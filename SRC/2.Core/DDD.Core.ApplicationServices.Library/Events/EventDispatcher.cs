using DDD.Core.Contracts.Library.ApplicationServices.Events;
using DDD.Core.Domain.Library.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DDD.Core.ApplicationServices.Library.Events;

/// <summary>
/// 
/// </summary>
public class EventDispatcher : IEventDispatcher
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<EventDispatcher> _logger;

    /// <summary>
    /// 
    /// </summary>
    private readonly Stopwatch _stopwatch;
    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="logger"></param>
    public EventDispatcher(IServiceProvider serviceProvider, ILogger<EventDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _stopwatch = new Stopwatch();
    }
    #endregion

    #region Event Dispatcher

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    /// <param name="event"></param>
    /// <returns></returns>
    public async Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent
    {
        _stopwatch.Start();
        int counter = 0;
        try
        {
            _logger.LogDebug("Routing event of type {EventType} With value {Event}  Start at {StartDateTime}", @event.GetType(), @event, DateTime.Now);
            var handlers = _serviceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
            List<Task> tasks = new List<Task>();
            foreach (var handler in handlers)
            {
                counter++;
                tasks.Add(handler.Handle(@event));
            }
            await Task.WhenAll(tasks);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "There is not suitable handler for {EventType} Routing failed at {StartDateTime}.", @event.GetType(), DateTime.Now);
            throw;
        }
        finally
        {
            _stopwatch.Stop();
            _logger.LogDebug("Total number of handler for {EventType} is {Count} ,EventHandlers tooks {Millisecconds} Millisecconds", @event.GetType(), counter, _stopwatch.ElapsedMilliseconds);
        }
    }
    #endregion
}