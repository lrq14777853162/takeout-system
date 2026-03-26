using Volo.Abp.Domain.Entities;

namespace FoodDelivery.Products;

/// <summary>商品规格</summary>
public class ProductSpec : Entity<Guid>
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal PriceAdjustment { get; set; }

    protected ProductSpec() { }

    public ProductSpec(Guid id, Guid productId, string name, decimal priceAdjustment = 0) : base(id)
    {
        ProductId = productId;
        Name = name;
        PriceAdjustment = priceAdjustment;
    }
}
