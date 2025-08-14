using BaseSource.Core.Domain.Common.Exceptions;

namespace BaseSource.Infrastructure.SQL.Query.Exceptions;

public class InfrastructureQueryException : BaseException
{
    public InfrastructureQueryException(string message) : base(message)
    {
    }

    public InfrastructureQueryException(string message, params string[] parameters) : base(message, parameters)
    {
    }
}
