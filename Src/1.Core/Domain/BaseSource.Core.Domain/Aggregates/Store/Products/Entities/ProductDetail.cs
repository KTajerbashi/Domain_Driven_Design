namespace BaseSource.Core.Domain.Aggregates.Store.Products.Entities;

// Domain/Entities/ProductDetail.cs
[Table("ProductDetails", Schema = "Store")]
public class ProductDetail : Entity
{
    public long ProductId { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }

    private ProductDetail() { }

    public ProductDetail(string key, string value)
    {
        Key = key;
        Value = value;

        Validate();
    }

    public void UpdateValue(string value)
    {
        Value = value;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Key))
            throw new DomainException("Detail key is required");

        if (string.IsNullOrWhiteSpace(Value))
            throw new DomainException("Detail value is required");
    }
}
