using BaseSource.Core.Application.Common.Handlers.Event;
using BaseSource.Core.Domain.Aggregates.Store.Products.Events;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Events;

public class ProductStockUpdatedEventHandler : DomainEventHandler<ProductStockUpdatedEvent>
{
    private readonly ILogger<ProductStockUpdatedEventHandler> _logger;
    public ProductStockUpdatedEventHandler(ProviderFactory factory, ILogger<ProductStockUpdatedEventHandler> logger) : base(factory)
    {
        _logger = logger;
    }

    public override async Task Handle(ProductStockUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProductStockUpdatedEventHandler\n");
        await Task.CompletedTask;
    }
}
