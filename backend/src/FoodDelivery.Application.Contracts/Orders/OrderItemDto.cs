using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Orders;

public class OrderItemDto : EntityDto<Guid>
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductImage { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? SpecInfo { get; set; }
    public decimal Subtotal => Price * Quantity;
}
