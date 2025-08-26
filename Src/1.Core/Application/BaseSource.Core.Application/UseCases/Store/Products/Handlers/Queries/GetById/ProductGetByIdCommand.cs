using BaseSource.Core.Application.UseCases.Store.Products.Repositories;
using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;
using static BaseSource.Core.Application.UseCases.Store.Products.Handlers.Commands.Create.ProductCategoryRoot;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Queries.GetById;

public class ProductGetByIdResponse : BaseDTO
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public string Sku { get; set; } = default!;

    public ProductCategoryModel ProductCategory { get; set; } = default!;
    public ProductWeightModel ProductWeight { get; set; } = default!;
    public ProductDimensionsModel ProductDimensions { get; set; } = default!;

    public List<ProductDetailModel> ProductDetails { get; set; } = new();
}
public class ProductGetByIdQuery : IQuery<ProductGetByIdResponse>
{
    public ProductGetByIdQuery(Guid entityId)
    {
        EntityId = entityId;
    }

    public Guid EntityId { get; set; }
}
public class ProductGetByIdHandler : QueryHandler<ProductGetByIdQuery, ProductGetByIdResponse>
{
    private readonly IProductQRepository _repository;
    public ProductGetByIdHandler(ProviderFactory factory, IProductQRepository repository) : base(factory)
    {
        _repository = repository;
    }

    public override async Task<ProductGetByIdResponse> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var resultGraph = await _repository.GetGraphAsync(request.EntityId, cancellationToken);

            if (resultGraph is null)
                throw new KeyNotFoundException($"Product with Id {request.EntityId} not found.");

            var response = Factory.Mapper.Map<Product, ProductGetByIdResponse>(resultGraph);
            return response;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductGetByIdResponse>()
            .ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.ProductWeight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.ProductDimensions, opt => opt.MapFrom(src => src.Dimensions))
            .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.Details));

        CreateMap<ProductCategory, ProductCategoryModel>().ReverseMap();
        CreateMap<ProductWeight, ProductWeightModel>().ReverseMap();
        CreateMap<ProductDimensions, ProductDimensionsModel>().ReverseMap();
        CreateMap<ProductDetail, ProductDetailModel>().ReverseMap();
    }
}