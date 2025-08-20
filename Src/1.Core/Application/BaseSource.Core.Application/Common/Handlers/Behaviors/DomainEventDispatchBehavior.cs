using BaseSource.Core.Domain.Common.Aggregate;
using FluentValidation;
using MediatR;

namespace BaseSource.Core.Application.Common.Handlers.Behaviors;

//public class DomainEventDispatchBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//{
//    private readonly ILogger<DomainEventDispatchBehavior<TRequest, TResponse>> _logger;
//    private readonly IPublisher _publisher;
//    private readonly ApplicationDbContext _context;

//    public DomainEventDispatchBehavior(
//        ILogger<DomainEventDispatchBehavior<TRequest, TResponse>> logger,
//        IPublisher publisher,
//        ApplicationDbContext context)
//    {
//        _logger = logger;
//        _publisher = publisher;
//        _context = context;
//    }

//    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        var response = await next();

//        // Dispatch domain events after the main operation
//        await DispatchDomainEventsAsync(cancellationToken);

//        return response;
//    }

//    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
//    {
//        var domainEntities = _context.ChangeTracker
//            .Entries<AggregateRoot>()
//            .Where(x => x.Entity.DomainEvents.Any())
//            .ToList();

//        var domainEvents = domainEntities
//            .SelectMany(x => x.Entity.DomainEvents)
//            .ToList();

//        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

//        foreach (var domainEvent in domainEvents)
//        {
//            _logger.LogDebug("Dispatching domain event: {EventType}", domainEvent.GetType().Name);

//            await _publisher.Publish(domainEvent, cancellationToken);
//        }

//        if (domainEvents.Any())
//        {
//            _logger.LogInformation("Dispatched {EventCount} domain events", domainEvents.Count);
//        }
//    }
//}