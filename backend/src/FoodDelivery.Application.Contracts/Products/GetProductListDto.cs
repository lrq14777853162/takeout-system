using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Products;

public class GetProductListDto : PagedAndSortedResultRequestDto
{
    public Guid? MerchantId { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Keyword { get; set; }
    public bool? IsAvailable { get; set; }
}
