using FoodDelivery.Enums;
using Volo.Abp.Domain.Entities;

namespace FoodDelivery.Orders;

/// <summary>订单状态日志</summary>
public class OrderStatusLog : Entity<Guid>
{
    public Guid OrderId { get; set; }
    public OrderStatus FromStatus { get; set; }
    public OrderStatus ToStatus { get; set; }
    public Guid? OperatorId { get; set; }
    public string? OperatorRole { get; set; }
    public string? Remark { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    protected OrderStatusLog() { }

    public OrderStatusLog(Guid id, Guid orderId, OrderStatus from, OrderStatus to, Guid? operatorId = null, string? role = null) : base(id)
    {
        OrderId = orderId;
        FromStatus = from;
        ToStatus = to;
        OperatorId = operatorId;
        OperatorRole = role;
    }
}
