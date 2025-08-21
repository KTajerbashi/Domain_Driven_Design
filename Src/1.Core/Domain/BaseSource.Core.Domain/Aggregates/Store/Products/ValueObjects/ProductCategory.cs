namespace BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;

// Domain/ValueObjects/ProductCategory.cs
public record ProductCategory
{
    public string Name { get; }
    public string Slug { get; }
    public long? ParentCategoryId { get; }

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
}

// Domain/ValueObjects/ProductWeight.cs
public record ProductWeight
{
    public decimal Value { get; }
    public WeightUnit Unit { get; }

    public ProductWeight(decimal value, WeightUnit unit)
    {
        Value = value;
        Unit = unit;

        Validate();
    }

    private void Validate()
    {
        if (Value <= 0)
            throw new DomainException("Weight must be greater than 0");
    }
}

public enum WeightUnit { Gram, Kilogram, Pound, Ounce }

// Domain/ValueObjects/ProductDimensions.cs
public record ProductDimensions
{
    public decimal Length { get; }
    public decimal Width { get; }
    public decimal Height { get; }
    public LengthUnit Unit { get; }

    public ProductDimensions(decimal length, decimal width, decimal height, LengthUnit unit)
    {
        Length = length;
        Width = width;
        Height = height;
        Unit = unit;

        Validate();
    }

    private void Validate()
    {
        if (Length <= 0 || Width <= 0 || Height <= 0)
            throw new DomainException("Dimensions must be greater than 0");
    }
}

public enum LengthUnit { Centimeter, Meter, Inch, Foot }