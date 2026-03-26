using FoodDelivery.Enums;
using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Orders;

public class GetOrderListDto : PagedAndSortedResultRequestDto
{
    public OrderStatus? Status { get; set; }
    public Guid? MerchantId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? RiderId { get; set; }
    public string? Keyword { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
