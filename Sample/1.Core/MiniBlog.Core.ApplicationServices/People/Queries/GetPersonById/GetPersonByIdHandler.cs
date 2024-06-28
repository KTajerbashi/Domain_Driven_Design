using DDD.Core.ApplicationServices.Library.Queries;
using DDD.Core.RequestResponse.Library.Queries;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.People.Queries;
using MiniBlog.Core.RequestResponse.People.Queries.GetAllPerson;
using MiniBlog.Core.RequestResponse.People.Queries.GetPersonById;

namespace MiniBlog.Core.ApplicationServices.People.Queries.GetPersonById;

public class GetPersonByIdHandler : QueryHandler<GetPersonByIdModel, PersonQuery>
{
    private readonly IPersonQueryRepository repository;
    public GetPersonByIdHandler(UtilitiesServices utilitiesServices,
        IPersonQueryRepository repository)
        : base(utilitiesServices)
    {
        this.repository = repository;
    }

    public override async Task<QueryResult<PersonQuery>> Handle(GetPersonByIdModel query)
    {
        var result = await repository.ExecuteAsync(query);
        return Result(result);
    }
}
