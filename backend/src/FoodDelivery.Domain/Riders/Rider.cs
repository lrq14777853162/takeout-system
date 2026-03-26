using FoodDelivery.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Riders;

/// <summary>骑手聚合根</summary>
public class Rider : FullAuditedAggregateRoot<Guid>
{
    public Guid UserId { get; set; }
    public string RealName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string IdCardNo { get; set; } = string.Empty;
    public string? IdCardFront { get; set; }
    public string? IdCardBack { get; set; }
    public RiderStatus Status { get; set; } = RiderStatus.Pending;
    public double? CurrentLat { get; set; }
    public double? CurrentLng { get; set; }
    public DateTime? LastLocationTime { get; set; }
    public decimal TotalEarnings { get; set; }
    public int TotalOrders { get; set; }
    public double Rating { get; set; } = 5.0;
    public decimal Balance { get; set; }

    protected Rider() { }

    public Rider(Guid id, Guid userId, string realName, string phone, string idCardNo) : base(id)
    {
        UserId = userId;
        RealName = realName;
        Phone = phone;
        IdCardNo = idCardNo;
    }

    public void UpdateLocation(double lat, double lng)
    {
        CurrentLat = lat;
        CurrentLng = lng;
        LastLocationTime = DateTime.UtcNow;
    }

    public void GoOnline() => Status = RiderStatus.Online;
    public void GoOffline() => Status = RiderStatus.Offline;
}
