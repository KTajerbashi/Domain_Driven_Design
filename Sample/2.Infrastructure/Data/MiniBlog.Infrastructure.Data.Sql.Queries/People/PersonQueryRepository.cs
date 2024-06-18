using DDD.Infra.Data.Sql.Queries.Library;
using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Contracts.People.Queries;
using MiniBlog.Core.RequestResponse.People.Queries.GetAllPerson;
using MiniBlog.Core.RequestResponse.People.Queries.GetPersonById;
using MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext;

namespace MiniBlog.Infrastructure.Data.Sql.Queries.People
{
    public class PersonQueryRepository : BaseQueryRepository<MiniBlogQueriesDbContext>, IPersonQueryRepository
    {
        public PersonQueryRepository(MiniBlogQueriesDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PersonQuery?> ExecuteAsync(GetPersonByIdModel query)
        {
            var result =  await _dbContext.People.Select(item => new PersonQuery()
            {
                Id = item.Id,
                FirstName = item.FirstName.Value,
                LastName = item.LastName.Value
            }).FirstOrDefaultAsync(code => code.Id.Equals(query.PersonId));
            return result;
        }
    }
}
