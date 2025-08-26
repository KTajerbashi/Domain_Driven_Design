using BaseSource.Core.Application.UseCases.Store.Products.Repositories;
using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Products.Parameters;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Commands.Create;

public class ProductCreateHandler : CommandHandler<ProductCreateCommand, ProductCreateResponse>
{
    private readonly IProductCRepository _repository;
    public ProductCreateHandler(ProviderFactory factory, IProductCRepository repository) : base(factory)
    {
        _repository = repository;
    }

    public override async Task<ProductCreateResponse> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var parameter = Factory.Mapper.Map<ProductCreateCommand, ProductCreateParameters>(request);
            Product entity = new Product(
                parameter,
                request.ProductCategory.ValueObject(),
                request.ProductWeight.ValueObject(),
                request.ProductDimensions.ValueObject()
                );

            request.ProductDetails.ForEach(item => entity.AddDetail(item.Key, item.Value));

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            await Factory.EventDispatcher.DispatchEventsAsync(entity, cancellationToken);
            return new ProductCreateResponse(entity.Id, entity.EntityId.ToValue());
        }
        catch (Exception ex)
        {
            throw new AppException(ex.Message);
        }
    }
}