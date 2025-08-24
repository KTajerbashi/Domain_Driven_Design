using BaseSource.Core.Application.Common.Handlers.Event;
using BaseSource.Core.Domain.Aggregates.Store.Products.Events;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Events;

public class ProductDetailDeletedEventHandler : DomainEventHandler<ProductDetailDeletedEvent>
{
    private readonly ILogger<ProductDetailDeletedEventHandler> _logger;
    public ProductDetailDeletedEventHandler(ProviderFactory factory, ILogger<ProductDetailDeletedEventHandler> logger) : base(factory)
    {
        _logger = logger;
    }

    public override async Task Handle(ProductDetailDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProductDetailDeletedEventHandler\n");
        await Task.CompletedTask;
    }
}
