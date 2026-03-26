using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Merchants;

/// <summary>商家分类</summary>
public class MerchantCategory : FullAuditedEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public int SortOrder { get; set; }

    protected MerchantCategory() { }

    public MerchantCategory(Guid id, string name, int sortOrder = 0) : base(id)
    {
        Name = name;
        SortOrder = sortOrder;
    }
}
