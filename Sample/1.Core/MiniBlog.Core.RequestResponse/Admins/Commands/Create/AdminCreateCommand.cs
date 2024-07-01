using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.Admins.Commands.Create;

public sealed class AdminCreateCommand : ICommand<long>
{
    public string Title { get; set; }
    public int RoleId { get; set; }
    public long CourseId { get; set; }
}
