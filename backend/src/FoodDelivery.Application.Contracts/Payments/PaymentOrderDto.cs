using FoodDelivery.Enums;
using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Payments;

public class PaymentOrderDto : FullAuditedEntityDto<Guid>
{
    public Guid OrderId { get; set; }
    public string OrderNo { get; set; } = string.Empty;
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public string? TransactionId { get; set; }
    public DateTime? PaidTime { get; set; }
}
