namespace BaseSource.Core.Application.UseCases.Store.Orders.Models;

public class OrderDto : BaseDTO
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string ShippingStreet { get; set; } = string.Empty;
    public string ShippingCity { get; set; } = string.Empty;
    public string ShippingCountry { get; set; } = string.Empty;
    public List<OrderItemDto> Items { get; set; } = new();
    public List<OrderStatusHistoryDto> StatusHistory { get; set; } = new();
}

public class OrderItemDto : BaseDTO
{
    public long ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}

public class OrderStatusHistoryDto : BaseDTO
{
    public string Status { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}