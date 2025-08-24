using BaseSource.Core.Application.Common.Handlers.Event;
using BaseSource.Core.Domain.Aggregates.Store.Products.Events;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Events;

public class ProductImageAddedEventHandler : DomainEventHandler<ProductImageAddedEvent>
{
    private readonly ILogger<ProductImageAddedEventHandler> _logger;
    public ProductImageAddedEventHandler(ProviderFactory factory, ILogger<ProductImageAddedEventHandler> logger) : base(factory)
    {
        _logger = logger;
    }

    public override async Task Handle(ProductImageAddedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProductImageAddedEventHandler\n");
        await Task.CompletedTask;
    }
}
