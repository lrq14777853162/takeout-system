using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Reviews;

/// <summary>评价</summary>
public class Review : FullAuditedAggregateRoot<Guid>
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public Guid MerchantId { get; set; }
    public Guid? RiderId { get; set; }
    public int MerchantRating { get; set; } // 1-5
    public int RiderRating { get; set; } // 1-5
    public string? Content { get; set; }
    public string? Images { get; set; } // JSON数组
    public string? MerchantReply { get; set; }
    public DateTime? MerchantReplyTime { get; set; }

    protected Review() { }

    public Review(Guid id, Guid orderId, Guid userId, Guid merchantId, int merchantRating) : base(id)
    {
        OrderId = orderId;
        UserId = userId;
        MerchantId = merchantId;
        MerchantRating = merchantRating;
    }
}
