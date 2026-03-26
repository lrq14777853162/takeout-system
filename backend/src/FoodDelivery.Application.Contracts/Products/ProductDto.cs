using Volo.Abp.Application.Dtos;

namespace FoodDelivery.Products;

public class ProductDto : FullAuditedEntityDto<Guid>
{
    public Guid MerchantId { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal PackingFee { get; set; }
    public int Stock { get; set; }
    public int MonthlySales { get; set; }
    public int SortOrder { get; set; }
    public bool IsAvailable { get; set; }
    public List<ProductSpecDto> Specs { get; set; } = [];
}

public class ProductSpecDto : EntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public decimal PriceAdjustment { get; set; }
}
