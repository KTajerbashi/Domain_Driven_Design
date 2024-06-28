using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniBlog.Core.Domain.People.ValueObjects;

namespace MiniBlog.Infrastructure.Data.Sql.ValueConversions
{
    public class FirstNameConversion : ValueConverter<FirstName, string>
    {
        public FirstNameConversion() : base(c => c.Value, c => FirstName.FromString(c))
        {

        }
    }
}
