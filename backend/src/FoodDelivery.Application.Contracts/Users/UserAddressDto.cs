using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Users;

public class UserAddressDto : FullAuditedEntityDto<Guid>
{
    public string ContactName { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string DetailAddress { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsDefault { get; set; }
    public string? Tag { get; set; }
    public string FullAddress => $"{Province}{City}{District}{DetailAddress}";
}
