using BaseSource.Core.Application.Common.Handlers.Event;
using BaseSource.Core.Application.UseCases.Store.Products.Repositories;
using BaseSource.Core.Domain.Aggregates.Store.Products.Events;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Events;

public class ProductCreatedEventHandler : DomainEventHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;
    private readonly IProductCRepository _repository;
    public ProductCreatedEventHandler(ProviderFactory factory, ILogger<ProductCreatedEventHandler> logger, IProductCRepository repository) : base(factory)
    {
        _logger = logger;
        _repository = repository;
    }

    public override async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProductCreatedEventHandler\n");
        var entity = await _repository.GetAsync(notification.EntityId, cancellationToken);
        entity.UpdateStock(10);
        await _repository.SaveChangesAsync(cancellationToken);
        await Task.CompletedTask;
    }
}
