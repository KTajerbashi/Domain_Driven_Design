namespace DDD.Core.Domain.Library.Exceptions;

public class InvalidEntityStateException : DomainStateException
{
    public InvalidEntityStateException(string message, params string[] parameters) : base(message, parameters)
    {
    }
}
