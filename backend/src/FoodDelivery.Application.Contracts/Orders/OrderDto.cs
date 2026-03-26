using FoodDelivery.Enums;
using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Orders;

public class OrderDto : FullAuditedEntityDto<Guid>
{
    public string OrderNo { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public Guid MerchantId { get; set; }
    public string MerchantName { get; set; } = string.Empty;
    public Guid? RiderId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal PackingFee { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal ActualAmount { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string? Remark { get; set; }
    public DateTime? PaidTime { get; set; }
    public DateTime? AcceptedTime { get; set; }
    public DateTime? PickedUpTime { get; set; }
    public DateTime? DeliveredTime { get; set; }
    public DateTime? ExpectedDeliveryTime { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
}
