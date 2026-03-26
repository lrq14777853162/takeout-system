using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Reviews;

public class ReviewDto : FullAuditedEntityDto<Guid>
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public Guid MerchantId { get; set; }
    public Guid? RiderId { get; set; }
    public int MerchantRating { get; set; }
    public int RiderRating { get; set; }
    public string? Content { get; set; }
    public List<string> Images { get; set; } = [];
    public string? MerchantReply { get; set; }
    public DateTime? MerchantReplyTime { get; set; }
}
