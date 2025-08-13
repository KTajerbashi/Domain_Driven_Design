using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Core.Domain.Aggregates.Commercial.ProductAggregate;

[Table("Products", Schema = "Commercial")]
public class Product : AggregateRoot
{
    public string Title { get; private set; }

    private List<ProductDetail> _productDetails;
    public virtual List<ProductDetail> ProductDetails => _productDetails;
}

[Table("ProductDetails", Schema = "Commercial")]
public class ProductDetail : AuditableEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
}
