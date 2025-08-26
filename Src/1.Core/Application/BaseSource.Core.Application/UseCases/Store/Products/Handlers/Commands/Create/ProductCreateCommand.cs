using static BaseSource.Core.Application.UseCases.Store.Products.Handlers.Commands.Create.ProductCategoryRoot;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Commands.Create;
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
