using DDD.Core.Domain.ToolKits.Library.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Infra.Data.Sql.Commands.Library.ValueConversions
{
    public class NationalCodeConversion : ValueConverter<NationalCode, string>
    {
        public NationalCodeConversion() : base(c => c.Value, c => NationalCode.FromString(c))
        {

        }
    }
}
