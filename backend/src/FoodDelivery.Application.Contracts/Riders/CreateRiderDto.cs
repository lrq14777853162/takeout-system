using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Riders;

public class CreateRiderDto
{
    [Required, MaxLength(64)]
    public string RealName { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required, MaxLength(18)]
    public string IdCardNo { get; set; } = string.Empty;

    public string? IdCardFront { get; set; }
    public string? IdCardBack { get; set; }
}
