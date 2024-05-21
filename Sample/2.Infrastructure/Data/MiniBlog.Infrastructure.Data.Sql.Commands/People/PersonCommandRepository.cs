using DDD.Core.Data.Sql.Commands.Library;
using MiniBlog.Core.Contracts.People.Commands;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Infrastructure.Data.Sql.Commands.DatabaseContext;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.People;

public class PersonCommandRepository :
        BaseCommandRepository<Person, MiniBlogCommandsDbContext, int>,
        IPersonCommandRepository
{
    public PersonCommandRepository(MiniBlogCommandsDbContext dbContext) : base(dbContext)
    {
    }
}
