using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Products;

public class CreateUpdateProductDto
{
    public Guid MerchantId { get; set; }
    public Guid CategoryId { get; set; }

    [Required, MaxLength(128)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal PackingFee { get; set; }
    public int Stock { get; set; }
    public int SortOrder { get; set; }
    public bool IsAvailable { get; set; } = true;
    public List<CreateProductSpecDto> Specs { get; set; } = [];
}

public class CreateProductSpecDto
{
    public string Name { get; set; } = string.Empty;
    public decimal PriceAdjustment { get; set; }
}
