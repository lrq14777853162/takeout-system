using FoodDelivery.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Payments;

/// <summary>支付单</summary>
public class PaymentOrder : FullAuditedAggregateRoot<Guid>
{
    public Guid OrderId { get; set; }
    public string OrderNo { get; set; } = string.Empty;
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? TransactionId { get; set; }
    public DateTime? PaidTime { get; set; }

    protected PaymentOrder() { }

    public PaymentOrder(Guid id, Guid orderId, string orderNo, PaymentMethod method, decimal amount) : base(id)
    {
        OrderId = orderId;
        OrderNo = orderNo;
        PaymentMethod = method;
        Amount = amount;
    }

    public void MarkSuccess(string transactionId)
    {
        Status = PaymentStatus.Success;
        TransactionId = transactionId;
        PaidTime = DateTime.UtcNow;
    }

    public void MarkFailed() => Status = PaymentStatus.Failed;
    public void MarkRefunded() => Status = PaymentStatus.Refunded;
}
