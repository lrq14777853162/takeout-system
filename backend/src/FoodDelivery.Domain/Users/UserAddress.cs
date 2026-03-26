using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Users;

/// <summary>用户地址</summary>
public class UserAddress : FullAuditedAggregateRoot<Guid>
{
    public Guid UserId { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string DetailAddress { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsDefault { get; set; }
    public string? Tag { get; set; } // 家/公司/学校

    protected UserAddress() { }

    public UserAddress(Guid id, Guid userId, string contactName, string contactPhone, string detailAddress) : base(id)
    {
        UserId = userId;
        ContactName = contactName;
        ContactPhone = contactPhone;
        DetailAddress = detailAddress;
    }
}
