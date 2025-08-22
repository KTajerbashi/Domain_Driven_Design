using BaseSource.Core.Domain.Aggregates.Store.Products.Enums;

namespace BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;

// Domain/ValueObjects/ProductDimensions.cs
public class ProductDimensions : BaseValueObject<ProductDimensions>
{
    public decimal Length { get; }
    public decimal Width { get; }
    public decimal Height { get; }
    public LengthUnitEnum Unit { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Length;
        yield return Width;
        yield return Height;
        yield return Unit;
    }
    public ProductDimensions(decimal length, decimal width, decimal height, LengthUnitEnum unit)
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

    public static ProductDimensions FromString(string v)
    {
        throw new NotImplementedException();
    }
}

