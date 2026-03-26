using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Products;

/// <summary>店内商品分类</summary>
public class ProductCategory : FullAuditedEntity<Guid>
{
    public Guid MerchantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }

    protected ProductCategory() { }

    public ProductCategory(Guid id, Guid merchantId, string name, int sortOrder = 0) : base(id)
    {
        MerchantId = merchantId;
        Name = name;
        SortOrder = sortOrder;
    }
}
