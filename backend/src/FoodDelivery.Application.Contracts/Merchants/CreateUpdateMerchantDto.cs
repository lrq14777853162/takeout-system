using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Merchants;

public class CreateUpdateMerchantDto
{
    [Required, MaxLength(128)]
    public string Name { get; set; } = string.Empty;

    public string? Logo { get; set; }
    public string? Description { get; set; }

    [Required, MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required, MaxLength(512)]
    public string Address { get; set; } = string.Empty;

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public decimal MinOrderAmount { get; set; }
    public decimal DeliveryFee { get; set; }
    public int DeliveryTime { get; set; } = 30;
    public TimeOnly OpenTime { get; set; }
    public TimeOnly CloseTime { get; set; }
    public Guid CategoryId { get; set; }
    public string? BusinessLicense { get; set; }
    public string? FoodLicense { get; set; }
}
