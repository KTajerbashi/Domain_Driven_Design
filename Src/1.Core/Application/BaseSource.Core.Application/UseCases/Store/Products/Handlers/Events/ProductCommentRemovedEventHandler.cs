using BaseSource.Core.Application.Common.Handlers.Event;
using BaseSource.Core.Domain.Aggregates.Store.Products.Events;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Events;

public class ProductCommentRemovedEventHandler : DomainEventHandler<ProductCommentRemovedEvent>
{
    private readonly ILogger<ProductCommentRemovedEventHandler> _logger;
    public ProductCommentRemovedEventHandler(ProviderFactory factory, ILogger<ProductCommentRemovedEventHandler> logger) : base(factory)
    {
        _logger = logger;
    }

    public override async Task Handle(ProductCommentRemovedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProductCommentRemovedEventHandler\n");
        await Task.CompletedTask;
    }
}
