using DDD.Core.Contracts.Library.ApplicationServices.Events;
using DDD.Core.Domain.Library.Events;

namespace DDD.Core.ApplicationServices.Library.Events;

/// <summary>
/// 
/// </summary>
public abstract class EventDispatcherDecorator : IEventDispatcher
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    protected IEventDispatcher _eventDispatcher;
    /// <summary>
    /// 
    /// </summary>
    public abstract int Order { get; }
    #endregion

    #region Constructors
    public EventDispatcherDecorator() { }
    #endregion

    #region Abstract Send Commands
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    /// <param name="event"></param>
    /// <returns></returns>
    public abstract Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventDispatcher"></param>
    public void SetEventDispatcher(IEventDispatcher eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }
    #endregion
}