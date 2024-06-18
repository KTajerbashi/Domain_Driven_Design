using DDD.Core.Contracts.Library.Data.Queries;
using MiniBlog.Core.RequestResponse.People.Queries.GetAllPerson;
using MiniBlog.Core.RequestResponse.People.Queries.GetPersonById;

namespace MiniBlog.Core.Contracts.People.Queries
{
    public interface IPersonQueryRepository : IQueryRepository
    {
        public Task<PersonQuery?> ExecuteAsync(GetPersonByIdModel query);

    }
}
