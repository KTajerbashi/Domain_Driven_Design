using BaseSource.Core.Domain.Common.Exceptions;

namespace BaseSource.Infrastructure.SQL.Command.Exceptions;

public class InfrastructureCommandException : BaseException
{
    public InfrastructureCommandException(string message) : base(message)
    {
    }

    public InfrastructureCommandException(string message, params string[] parameters) : base(message, parameters)
    {
    }
}
