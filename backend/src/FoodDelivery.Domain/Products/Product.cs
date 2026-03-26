using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Products;

/// <summary>商品聚合根</summary>
public class Product : FullAuditedAggregateRoot<Guid>
{
    public Guid MerchantId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal PackingFee { get; set; }
    public int Stock { get; set; }
    public int MonthlySales { get; set; }
    public int SortOrder { get; set; }
    public bool IsAvailable { get; set; } = true;

    public virtual ICollection<ProductSpec> Specs { get; set; } = [];

    protected Product() { }

    public Product(Guid id, Guid merchantId, Guid categoryId, string name, decimal price) : base(id)
    {
        MerchantId = merchantId;
        CategoryId = categoryId;
        Name = name;
        Price = price;
        OriginalPrice = price;
    }
}
