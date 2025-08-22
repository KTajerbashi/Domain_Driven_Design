using BaseSource.Core.Domain.Aggregates.Store.Products.Enums;

namespace BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;

// Domain/ValueObjects/ProductWeight.cs
public class ProductWeight : BaseValueObject<ProductWeight>
{
    public decimal Value { get; }
    public WeightUnitEnum Unit { get; }

    public ProductWeight(decimal value, WeightUnitEnum unit)
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Unit;
    }

    public static ProductWeight FromString(string v)
    {
        throw new NotImplementedException();
    }
}

