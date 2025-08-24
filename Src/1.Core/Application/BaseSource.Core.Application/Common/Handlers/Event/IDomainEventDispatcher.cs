using BaseSource.Core.Application.Providers.Autofac;
using BaseSource.Core.Domain.Common.Aggregate;

namespace BaseSource.Core.Application.Common.Handlers.Event;

public interface IDomainEventDispatcher : IAutofacScopedLifetime
{
    Task DispatchEventsAsync(AggregateRoot aggregate, CancellationToken cancellationToken = default);
}
public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(IMediator mediator, ILogger<DomainEventDispatcher> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task DispatchEventsAsync(AggregateRoot aggregate, CancellationToken cancellationToken = default)
    {
        var events = aggregate.GetEvents().ToArray();

        _logger.LogInformation("Dispatching {EventCount} domain events", events.Length);

        foreach (var domainEvent in events)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }

        aggregate.ClearEvents();
    }
}