namespace BaseSource.Core.Domain.Aggregates.Store.Products;

public class Product : AggregateRoot
{
    public string Title { get; set; }
}
public class ProductDetail : Entity
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public class ProductComment : Entity
{
    public long CustomerId { get; set; }
    public long ProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}



