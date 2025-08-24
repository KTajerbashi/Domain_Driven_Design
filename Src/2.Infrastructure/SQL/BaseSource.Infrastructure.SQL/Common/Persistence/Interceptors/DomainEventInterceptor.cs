using BaseSource.Core.Application.Interfaces;
using BaseSource.Core.Application.Providers.Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BaseSource.Infrastructure.SQL.Common.Persistence.Interceptors;

public class DomainEventInterceptor : SaveChangesInterceptor, IAutofacSingletonLifetime
{
    public DomainEventInterceptor()
    {
        
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        var _mediator = eventData.Context?.GetService<IMediator>();
        
        if (_mediator == null)
            return result;
        
        if (eventData.Context == null) return result;

        var aggregates = eventData.Context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .ToList();

        foreach (var aggregate in aggregates)
        {
            var events = aggregate.GetEvents().ToArray();
            aggregate.ClearEvents();

            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }

        return result;
    }
}



