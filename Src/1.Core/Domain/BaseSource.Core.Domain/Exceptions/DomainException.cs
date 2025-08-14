namespace BaseSource.Core.Domain.Exceptions;

public class DomainException : BaseException
{
    public DomainException(string message) : base(message)
    {
    }

    public DomainException(DomainException exception) : base(exception)
    {
    }

    public DomainException(string message, params string[] parameters) : base(message, parameters)
    {
    }
}
