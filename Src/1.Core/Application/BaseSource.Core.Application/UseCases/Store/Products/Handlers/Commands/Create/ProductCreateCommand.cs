using BaseSource.Core.Application.UseCases.Store.Products.Repositories;
using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Products.Enums;
using BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Commands.Create;

public record ProductCreateResponse(long Id, Guid EntityId);


public class ProductCategoryModel
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public long? ParentCategoryId { get; set; }

    internal ProductCategory ValueObject() => new ProductCategory(Name, Slug, ParentCategoryId);
}
public class ProductWeightModel
{
    public decimal Value { get; set; }
    public WeightUnitEnum WeightUnit { get; set; }
    internal ProductWeight ValueObject() => new ProductWeight(Value, WeightUnit);
}
public class ProductDimensionsModel
{
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public LengthUnitEnum Unit { get; set; }
    internal ProductDimensions ValueObject() => new ProductDimensions(Length, Width, Height, Unit);
}
public record ProductDetailModel(string Key, string Value);
public class ProductCreateCommand : ICommand<ProductCreateResponse>
{
    public ProductCategoryModel ProductCategory { get; set; }
    public ProductWeightModel ProductWeight { get; set; }
    public ProductDimensionsModel ProductDimensions { get; set; }

    public List<ProductDetailModel> ProductDetails { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Sku { get; set; }
}
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
            Product entity = new Product(
                request.Title,
                request.Description,
                request.Price,
                request.Sku,
                request.ProductCategory.ValueObject(),
                request.ProductWeight.ValueObject(),
                request.ProductDimensions.ValueObject()
                );

            request.ProductDetails.ForEach(item => entity.AddDetail(item.Key, item.Value));

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            await Factory.EventDispatcher.DispatchEventsAsync(entity,cancellationToken);
            return new ProductCreateResponse(entity.Id, entity.EntityId.ToValue());
        }
        catch (Exception ex)
        {
            throw new AppException(ex.Message);
        }
    }
}