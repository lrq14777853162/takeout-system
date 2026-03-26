using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Reviews;

public class CreateReviewDto
{
    [Required]
    public Guid OrderId { get; set; }

    [Range(1, 5)]
    public int MerchantRating { get; set; } = 5;

    [Range(1, 5)]
    public int RiderRating { get; set; } = 5;

    [MaxLength(1024)]
    public string? Content { get; set; }

    public List<string> Images { get; set; } = [];
}
