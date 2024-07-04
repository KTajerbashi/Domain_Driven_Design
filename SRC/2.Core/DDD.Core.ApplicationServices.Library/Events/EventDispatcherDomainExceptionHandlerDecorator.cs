using DDD.Core.Domain.Library.Exceptions;
using Microsoft.Extensions.Logging;
using Extensions.Logger.Abstractions;

namespace DDD.Core.ApplicationServices.Library.Events;
/// <summary>
/// 
/// </summary>
public class EventDispatcherDomainExceptionHandlerDecorator : EventDispatcherDecorator
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<EventDispatcherDomainExceptionHandlerDecorator> _logger;
    /// <summary>
    /// 
    /// </summary>
    public override int Order => 2;
    #endregion

    #region Constructors
    public EventDispatcherDomainExceptionHandlerDecorator(ILogger<EventDispatcherDomainExceptionHandlerDecorator> logger)
    {
        _logger = logger;
    }
    #endregion

    #region Publish Domain Event
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    /// <param name="event"></param>
    /// <returns></returns>
    public override async Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event)
    {
        try
        {
            await _eventDispatcher.PublishDomainEventAsync(@event);
        }
        catch (DomainStateException ex)
        {
            _logger.LogError(LoggingEventId.DomainValidationException, ex, "Processing of {EventType} With value {Event} failed at {StartDateTime} because there are domain exceptions.", @event.GetType(), @event, DateTime.Now);
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is DomainStateException domainStateException)
            {
                _logger.LogError(LoggingEventId.DomainValidationException, ex, "Processing of {EventType} With value {Event} failed at {StartDateTime} because there are domain exceptions.", @event.GetType(), @event, DateTime.Now);
            }
            throw ex;
        }
    }

    public override async Task PublishDomainEventAsync<TDomainEvent>(IEnumerable<TDomainEvent> @events)
    {
        foreach (var @item in @events)
        {
            try
            {
                await _eventDispatcher.PublishDomainEventAsync(@item);
            }
            catch (DomainStateException ex)
            {
                _logger.LogError(LoggingEventId.DomainValidationException, ex, "Processing of {EventType} With value {Event} failed at {StartDateTime} because there are domain exceptions.", @item.GetType(), @item, DateTime.Now);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is DomainStateException domainStateException)
                {
                    _logger.LogError(LoggingEventId.DomainValidationException, ex, "Processing of {EventType} With value {Event} failed at {StartDateTime} because there are domain exceptions.", @item.GetType(), @item, DateTime.Now);
                }
                throw ex;
            }
        }
    }
    #endregion
}