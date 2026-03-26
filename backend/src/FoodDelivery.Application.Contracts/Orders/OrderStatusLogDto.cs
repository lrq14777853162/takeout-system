using FoodDelivery.Enums;
using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Orders;

public class OrderStatusLogDto : EntityDto<Guid>
{
    public Guid OrderId { get; set; }
    public OrderStatus FromStatus { get; set; }
    public OrderStatus ToStatus { get; set; }
    public Guid? OperatorId { get; set; }
    public string? OperatorRole { get; set; }
    public string? Remark { get; set; }
    public DateTime CreatedAt { get; set; }
}
