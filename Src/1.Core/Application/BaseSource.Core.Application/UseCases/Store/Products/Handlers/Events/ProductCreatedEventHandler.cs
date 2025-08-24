using BaseSource.Core.Application.Common.Handlers.Event;
using BaseSource.Core.Domain.Aggregates.Store.Products.Events;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Events;

public class ProductCreatedEventHandler : DomainEventHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;
    public ProductCreatedEventHandler(ProviderFactory factory, ILogger<ProductCreatedEventHandler> logger) : base(factory)
    {
        _logger = logger;
    }

    public override async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProductCreatedEventHandler\n");
        await Task.CompletedTask;
    }
}
