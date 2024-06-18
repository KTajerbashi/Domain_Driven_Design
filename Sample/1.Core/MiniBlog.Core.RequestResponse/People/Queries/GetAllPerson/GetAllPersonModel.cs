using DDD.Core.RequestResponse.Library.Queries;

namespace MiniBlog.Core.RequestResponse.People.Queries.GetAllPerson;

public class GetAllPersonModel : IQuery<int>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
