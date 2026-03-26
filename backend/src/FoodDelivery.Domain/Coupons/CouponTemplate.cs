using FoodDelivery.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Coupons;

/// <summary>优惠券模板</summary>
public class CouponTemplate : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public CouponType Type { get; set; }
    public decimal Value { get; set; }
    public decimal MinOrderAmount { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TotalCount { get; set; }
    public int RemainCount { get; set; }
    public int Scope { get; set; } // 0:全平台 1:指定商家
    public Guid? ScopeMerchantId { get; set; }

    protected CouponTemplate() { }

    public CouponTemplate(Guid id, string name, CouponType type, decimal value, decimal minOrderAmount) : base(id)
    {
        Name = name;
        Type = type;
        Value = value;
        MinOrderAmount = minOrderAmount;
    }
}
