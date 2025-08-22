using BaseSource.Core.Domain.Aggregates.Store.Orders.ValueObjects;
using BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;
using BaseSource.Core.Domain.ValueObjects.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BaseSource.Infrastructure.SQL.Conversions;

public class TitleConversion : ValueConverter<Title, string>
{
    public TitleConversion() : base(c => c.Value, c => Title.FromString(c))
    {

    }
}

public class DescriptionConversion : ValueConverter<Description, string>
{
    public DescriptionConversion() : base(c => c.Value, c => Description.FromString(c))
    {

    }
}





