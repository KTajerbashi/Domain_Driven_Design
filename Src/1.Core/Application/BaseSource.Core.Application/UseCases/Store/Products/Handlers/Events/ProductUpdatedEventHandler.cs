using BaseSource.Core.Application.Common.Handlers.Event;
using BaseSource.Core.Domain.Aggregates.Store.Products.Events;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Events;

public class ProductUpdatedEventHandler : DomainEventHandler<ProductUpdatedEvent>
{
    private readonly ILogger<ProductUpdatedEventHandler> _logger;
    public ProductUpdatedEventHandler(ProviderFactory factory, ILogger<ProductUpdatedEventHandler> logger) : base(factory)
    {
        _logger = logger;
    }

    public override async Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProductUpdatedEventHandler\n");
        await Task.CompletedTask;
    }
}
