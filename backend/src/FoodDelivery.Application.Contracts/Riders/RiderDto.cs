using FoodDelivery.Enums;
using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Riders;

public class RiderDto : FullAuditedEntityDto<Guid>
{
    public Guid UserId { get; set; }
    public string RealName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public RiderStatus Status { get; set; }
    public double? CurrentLat { get; set; }
    public double? CurrentLng { get; set; }
    public DateTime? LastLocationTime { get; set; }
    public decimal TotalEarnings { get; set; }
    public int TotalOrders { get; set; }
    public double Rating { get; set; }
    public decimal Balance { get; set; }
}
