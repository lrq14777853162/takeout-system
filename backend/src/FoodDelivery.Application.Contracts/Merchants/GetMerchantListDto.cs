using FoodDelivery.Enums;
using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Merchants;

public class GetMerchantListDto : PagedAndSortedResultRequestDto
{
    public MerchantStatus? Status { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Keyword { get; set; }
    public double? UserLat { get; set; }
    public double? UserLng { get; set; }
}
