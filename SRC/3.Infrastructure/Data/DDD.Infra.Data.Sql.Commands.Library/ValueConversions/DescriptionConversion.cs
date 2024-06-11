using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DDD.Core.Domain.ToolKits.Library.ValueObjects;

namespace DDD.Infra.Data.Sql.Commands.Library.ValueConversions
{
    public class DescriptionConversion : ValueConverter<Description, string>
    {
        public DescriptionConversion() : base(c => c.Value, c => Description.FromString(c))
        {

        }
    }
}
