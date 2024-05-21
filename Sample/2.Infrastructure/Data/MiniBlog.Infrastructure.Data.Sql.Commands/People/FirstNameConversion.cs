using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniBlog.Core.Domain.People.ValueObjects;
using System.Linq.Expressions;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.People
{
    public class FirstNameConversion : ValueConverter<FirstName, string>
    {
        public FirstNameConversion() : base(c => c.Value, c => FirstName.FromString(c))
        {

        }
    }
    public class LastNameConversion : ValueConverter<LastName, string>
    {
        public LastNameConversion() : base(c => c.Value, c => LastName.FromString(c))
        {

        }
    }
}
