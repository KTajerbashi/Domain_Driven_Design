using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DDD.Core.Domain.Library.ValueObjects;

namespace DDD.Infra.Data.Sql.Library.ValueConversions
{
    public class BusinessIdConversion : ValueConverter<BusinessId, Guid>
    {
        public BusinessIdConversion() : base(c => c.Value, c => BusinessId.FromGuid(c))
        {

        }
    }
}
