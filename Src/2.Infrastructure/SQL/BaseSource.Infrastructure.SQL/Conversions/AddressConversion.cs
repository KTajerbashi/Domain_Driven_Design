using BaseSource.Core.Domain.Aggregates.Store.Orders.ValueObjects;
using BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BaseSource.Infrastructure.SQL.Command.Conversions.Store;

public class AddressConversion : ValueConverter<Address, string>
{
    public AddressConversion() : base(
        v => v.ToString(),     // Convert Address to String for DB
        v => Address.FromString(v) // Convert String to Address from DB
    )
    { }
}
public class ProductCategoryConversion : ValueConverter<ProductCategory, string>
{
    public ProductCategoryConversion() : base(
        v => v.ToString(),     // Convert Address to String for DB
        v => ProductCategory.FromString(v) // Convert String to Address from DB
    )
    { }
}

public class ProductWeightConversion : ValueConverter<ProductWeight, string>
{
    public ProductWeightConversion() : base(
        v => v.ToString(),     // Convert Address to String for DB
        v => ProductWeight.FromString(v) // Convert String to Address from DB
    )
    { }
}


public class ProductDimensionsConversion : ValueConverter<ProductDimensions, string>
{
    public ProductDimensionsConversion() : base(
        v => v.ToString(),     // Convert Address to String for DB
        v => ProductDimensions.FromString(v) // Convert String to Address from DB
    )
    { }
}