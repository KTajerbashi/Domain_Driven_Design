using BaseSource.Core.Domain.Aggregates.Store.Products.Enums;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;

// Domain/ValueObjects/ProductDimensions.cs
public class ProductDimensions : BaseValueObject<ProductDimensions>
{
    public decimal Length { get; }
    public decimal Width { get; }
    public decimal Height { get; }
    public LengthUnitEnum Unit { get; }
    private void Validate()
    {
        if (Length <= 0 || Width <= 0 || Height <= 0)
            throw new DomainException("Dimensions must be greater than 0");
    }
    public ProductDimensions(decimal length, decimal width, decimal height, LengthUnitEnum unit)
    {
        Length = length;
        Width = width;
        Height = height;
        Unit = unit;

        Validate();
    }
    private ProductDimensions() { }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Length;
        yield return Width;
        yield return Height;
        yield return Unit;
    }


    
    public override string ToString()
    {
        return $"{Length}|{Width}|{Height}|{Unit}";
    }
    public static ProductDimensions FromString(string value)
    {
        if (string.IsNullOrEmpty(value))
            return new ProductDimensions(); // Or throw exception

        var result = value.Split('|');

        if (result.Length < 3)
            throw new ArgumentException("Invalid string format for ProductDimensions", nameof(value));

        decimal length;
        decimal.TryParse(result[0], out length);

        decimal width;
        decimal.TryParse(result[1], out width);

        decimal height;
        decimal.TryParse(result[2], out height);

        byte unit;
        byte.TryParse(result[3], out unit);




        return new ProductDimensions(length, width, height, unit.To<LengthUnitEnum>());
    }
}

