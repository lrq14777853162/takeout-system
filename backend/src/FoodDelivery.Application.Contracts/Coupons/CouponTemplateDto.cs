using FoodDelivery.Enums;
using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Coupons;

public class CouponTemplateDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public CouponType Type { get; set; }
    public decimal Value { get; set; }
    public decimal MinOrderAmount { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TotalCount { get; set; }
    public int RemainCount { get; set; }
    public int Scope { get; set; }
    public Guid? ScopeMerchantId { get; set; }
}
