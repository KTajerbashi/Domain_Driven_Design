
namespace BaseSource.Core.Application.Exceptions;

public class PaymentException : BaseException
{
    public PaymentException(string message) : base(message)
    {
    }
}
public class PaymentGatewayException : BaseException
{
    public PaymentGatewayException(string message) : base(message)
    {
    }
}
public class OrderNotFoundException : BaseException
{
    public OrderNotFoundException(long orderId)
        : base($"Order with ID {orderId} not found") { }
}

public class ProductNotFoundException : BaseException
{
    public ProductNotFoundException(long productId)
        : base($"Product with ID {productId} not found") { }
}
