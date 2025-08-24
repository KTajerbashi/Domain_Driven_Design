using BaseSource.Core.Application.Common.Handlers.Event;
using BaseSource.Core.Domain.Aggregates.Store.Products.Events;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Events;

public class ProductDetailAddedEventHandler : DomainEventHandler<ProductDetailAddedEvent>
{
    private readonly ILogger<ProductDetailAddedEventHandler> _logger;
    public ProductDetailAddedEventHandler(ProviderFactory factory, ILogger<ProductDetailAddedEventHandler> logger) : base(factory)
    {
        _logger = logger;
    }

    public override async Task Handle(ProductDetailAddedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProductDetailAddedEventHandler\n");
        await Task.CompletedTask;
    }
}
