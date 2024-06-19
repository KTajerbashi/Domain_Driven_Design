namespace MiniBlog.Core.RequestResponse.People.Queries.GetAllPerson;

public class PersonQuery
{
    public int Id { get; set; }
    public Guid BusinessId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}