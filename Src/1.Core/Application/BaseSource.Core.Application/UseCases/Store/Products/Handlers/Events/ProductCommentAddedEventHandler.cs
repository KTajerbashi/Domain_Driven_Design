using BaseSource.Core.Application.Common.Handlers.Event;
using BaseSource.Core.Domain.Aggregates.Store.Products.Events;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Events;

public class ProductCommentAddedEventHandler : DomainEventHandler<ProductCommentAddedEvent>
{
    private readonly ILogger<ProductCommentAddedEventHandler> _logger;
    public ProductCommentAddedEventHandler(ProviderFactory factory, ILogger<ProductCommentAddedEventHandler> logger) : base(factory)
    {
        _logger = logger;
    }

    public override async Task Handle(ProductCommentAddedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProductCommentAddedEventHandler\n");
        await Task.CompletedTask;
    }
}
