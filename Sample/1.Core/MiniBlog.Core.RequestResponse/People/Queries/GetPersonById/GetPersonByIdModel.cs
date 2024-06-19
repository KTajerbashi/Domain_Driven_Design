using DDD.Core.RequestResponse.Library.EndPoints;
using DDD.Core.RequestResponse.Library.Queries;
using MiniBlog.Core.RequestResponse.People.Queries.GetAllPerson;

namespace MiniBlog.Core.RequestResponse.People.Queries.GetPersonById;

public class GetPersonByIdModel : IQuery<PersonQuery>, IWebRequest
{
    public int PersonId { get; set; }
    public string Path => "/api/Person/GetPersonById";
}
