using FoodDelivery.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Coupons;

/// <summary>用户优惠券</summary>
public class UserCoupon : FullAuditedAggregateRoot<Guid>
{
    public Guid UserId { get; set; }
    public Guid CouponTemplateId { get; set; }
    public UserCouponStatus Status { get; set; } = UserCouponStatus.Unused;
    public Guid? UsedOrderId { get; set; }
    public DateTime? UsedTime { get; set; }

    protected UserCoupon() { }

    public UserCoupon(Guid id, Guid userId, Guid couponTemplateId) : base(id)
    {
        UserId = userId;
        CouponTemplateId = couponTemplateId;
    }
}
