using DDD.Infra.Data.Sql.Queries.Library;
using MiniBlog.Core.Contracts.People.Queries;
using MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext;

namespace MiniBlog.Infrastructure.Data.Sql.Queries.People
{
    public class PersonQueryRepository : BaseQueryRepository<MiniBlogQueriesDbContext>, IPersonQueryRepository
    {
        public PersonQueryRepository(MiniBlogQueriesDbContext dbContext) : base(dbContext)
        {
        }
    }
}
