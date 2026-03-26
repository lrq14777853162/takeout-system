using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Payments;

/// <summary>退款单</summary>
public class RefundOrder : FullAuditedAggregateRoot<Guid>
{
    public Guid OrderId { get; set; }
    public Guid PaymentOrderId { get; set; }
    public decimal RefundAmount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public int Status { get; set; } // 0:待处理 1:已退款 2:已拒绝
    public DateTime? ProcessedTime { get; set; }
    public Guid? ProcessedBy { get; set; }

    protected RefundOrder() { }

    public RefundOrder(Guid id, Guid orderId, Guid paymentOrderId, decimal amount, string reason) : base(id)
    {
        OrderId = orderId;
        PaymentOrderId = paymentOrderId;
        RefundAmount = amount;
        Reason = reason;
    }
}
