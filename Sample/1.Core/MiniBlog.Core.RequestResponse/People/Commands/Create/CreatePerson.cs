using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.People.Commands.Create
{
    public class CreatePerson : ICommand<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
