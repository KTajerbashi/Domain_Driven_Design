using BaseSource.Core.Domain.Common.Exceptions;

namespace BaseSource.EndPoint.WebApi.Exceptions;

public class WebApiException : BaseException
{
    public WebApiException(string message) : base(message)
    {
    }
}