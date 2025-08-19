
namespace BaseSource.Core.Application.Exceptions;

public class AppException : BaseException
{
    public AppException(string message) : base(message)
    {
    }
}
public class PaymentException : AppException
{
    public PaymentException(string message) : base(message)
    {
    }
}
public class PaymentGatewayException : AppException
{
    public PaymentGatewayException(string message) : base(message)
    {
    }
}
public class OrderNotFoundException : AppException
{
    public OrderNotFoundException(long orderId)
        : base($"Order with ID {orderId} not found") { }
}

public class ProductNotFoundException : AppException
{
    public ProductNotFoundException(long productId)
        : base($"Product with ID {productId} not found") { }
}
