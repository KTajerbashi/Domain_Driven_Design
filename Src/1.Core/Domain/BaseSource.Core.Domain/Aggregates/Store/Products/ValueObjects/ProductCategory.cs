
namespace BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;

// Domain/ValueObjects/ProductCategory.cs
public class ProductCategory : BaseValueObject<ProductCategory>
{
    public string Name { get; }
    public string Slug { get; }
    public long? ParentCategoryId { get; }

    private ProductCategory()
    {
        
    }
    public ProductCategory(string name, string slug, long? parentCategoryId = null)
    {
        Name = name;
        Slug = slug;
        ParentCategoryId = parentCategoryId;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new DomainException("Category name is required");

        if (string.IsNullOrWhiteSpace(Slug))
            throw new DomainException("Category slug is required");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Slug;
        yield return ParentCategoryId ?? null;
    }

    public override string ToString()
    {
        return $"{Name}|{Slug}|{ParentCategoryId}";
    }
    public static ProductCategory FromString(string value)
    {
        if (string.IsNullOrEmpty(value))
            return new ProductCategory(); // Or throw exception

        var result = value.Split('|');

        if (result.Length < 3)
            throw new ArgumentException("Invalid string format for ProductCategory", nameof(value));

        var name = result[0];
        var slug = result[1];

        // Handle null/empty parent category ID
        long? parentCategoryId = null;
        if (!string.IsNullOrEmpty(result[2]) && result[2] != "null")
        {
            if (long.TryParse(result[2], out long parsedId))
            {
                parentCategoryId = parsedId;
            }
        }

        return new ProductCategory(name, slug, parentCategoryId);
    }
}

