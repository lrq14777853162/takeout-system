using FoodDelivery.Enums;
using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Merchants;

public class MerchantDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Logo { get; set; }
    public string? Description { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public MerchantStatus Status { get; set; }
    public decimal MinOrderAmount { get; set; }
    public decimal DeliveryFee { get; set; }
    public int DeliveryTime { get; set; }
    public double Rating { get; set; }
    public int MonthlySales { get; set; }
    public TimeOnly OpenTime { get; set; }
    public TimeOnly CloseTime { get; set; }
    public Guid OwnerId { get; set; }
    public Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }
}
