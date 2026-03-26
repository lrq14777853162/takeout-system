using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Users;

public class CreateUpdateUserAddressDto
{
    [Required, MaxLength(64)]
    public string ContactName { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string ContactPhone { get; set; } = string.Empty;

    [MaxLength(32)] public string Province { get; set; } = string.Empty;
    [MaxLength(32)] public string City { get; set; } = string.Empty;
    [MaxLength(32)] public string District { get; set; } = string.Empty;

    [Required, MaxLength(512)]
    public string DetailAddress { get; set; } = string.Empty;

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsDefault { get; set; }
    public string? Tag { get; set; }
}
