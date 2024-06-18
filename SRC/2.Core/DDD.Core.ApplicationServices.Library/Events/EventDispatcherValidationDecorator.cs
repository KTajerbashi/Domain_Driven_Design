using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Extensions.Logger.Abstractions;

namespace DDD.Core.ApplicationServices.Library.Events;
/// <summary>
/// 
/// </summary>
public class EventDispatcherValidationDecorator : EventDispatcherDecorator
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    private readonly IServiceProvider _serviceProvider;
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<EventDispatcherValidationDecorator> _logger;
    /// <summary>
    /// 
    /// </summary>
    public override int Order => 1;
    #endregion

    #region Constructors
    public EventDispatcherValidationDecorator(IServiceProvider serviceProvider,
                                              ILogger<EventDispatcherValidationDecorator> logger)
    {
        _serviceProvider = serviceProvider;
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
        _logger.LogDebug(LoggingEventId.EventValidation, "Validating Event of type {EventType} With value {Event}  start at :{StartDateTime}", @event.GetType(), @event, DateTime.Now);

        List<string> errorMessages = Validate(@event);

        if (errorMessages.Any())
        {
            _logger.LogInformation(LoggingEventId.EventValidation, "Validating query of type {QueryType} With value {Query}  failed. Validation errors are: {ValidationErrors}", @event.GetType(), @event, errorMessages);
        }
        else
        {
            _logger.LogDebug(LoggingEventId.EventValidation, "Validating query of type {QueryType} With value {Query}  finished at :{EndDateTime}", @event.GetType(), @event, DateTime.Now);
            await _eventDispatcher.PublishDomainEventAsync(@event);
        }
    }
    #endregion

    #region Privaite Methods
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    /// <param name="event"></param>
    /// <returns></returns>
    private List<string> Validate<TDomainEvent>(TDomainEvent @event)
    {
        List<string> errorMessages = new();

        var validator = _serviceProvider.GetService<IValidator<TDomainEvent>>();

        if (validator != null)
        {
            var validationResult = validator.Validate(@event);
            if (!validationResult.IsValid)
                errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
        }
        else
        {
            _logger.LogInformation(LoggingEventId.CommandValidation, "There is not any validator for {EventType}", @event.GetType());
        }

        return errorMessages;
    }
    #endregion
}