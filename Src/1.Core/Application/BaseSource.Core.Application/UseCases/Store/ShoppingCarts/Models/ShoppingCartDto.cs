namespace BaseSource.Core.Application.UseCases.Store.ShoppingCarts.Models;

public class ShoppingCartDto : BaseDTO
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public decimal TotalPrice { get; set; }
    public int TotalItems { get; set; }
    public List<CartItemDto> Items { get; set; } = new();
}

public class CartItemDto : BaseDTO
{
    public long ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string ProductImage { get; set; } = string.Empty;
    public bool ProductInStock { get; set; }
}