using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.Admins.Commands.Delete;

public sealed class AdminDeleteCommand : ICommand<long>
{
    public string Name { get; set; }
    public int Length { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
