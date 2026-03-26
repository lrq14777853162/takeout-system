using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Orders;

public class CreateOrderDto
{
    [Required]
    public Guid MerchantId { get; set; }

    [Required]
    public Guid AddressId { get; set; }

    public Guid? CouponId { get; set; }

    public string? Remark { get; set; }

    [Required]
    [MinLength(1)]
    public List<CreateOrderItemDto> Items { get; set; } = [];
}

public class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid? SpecId { get; set; }
}
