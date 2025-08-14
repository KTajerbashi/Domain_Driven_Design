using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BaseSource.Infrastructure.SQL.Common.Persistence.Conversions;

public class EntityIdConversion : ValueConverter<EntityId, Guid>
{
    public EntityIdConversion() : base(c => c.Value, c => EntityId.FromGuid(c))
    {

    }
}