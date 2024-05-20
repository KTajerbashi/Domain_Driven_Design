using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.People.Commands.ChangeName
{
    public class ChangePersonName : ICommand<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}
