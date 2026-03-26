using FoodDelivery.Enums;
using FoodDelivery.Products;
using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Merchants;

/// <summary>商家聚合根</summary>
public class Merchant : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Logo { get; set; }
    public string? Description { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public MerchantStatus Status { get; set; } = MerchantStatus.Pending;
    public decimal MinOrderAmount { get; set; }
    public decimal DeliveryFee { get; set; }
    public int DeliveryTime { get; set; } // 分钟
    public double Rating { get; set; }
    public int MonthlySales { get; set; }
    public string? BusinessLicense { get; set; }
    public string? FoodLicense { get; set; }
    public TimeOnly OpenTime { get; set; }
    public TimeOnly CloseTime { get; set; }
    public Guid OwnerId { get; set; }
    public Guid CategoryId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = [];
    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = [];

    protected Merchant() { }

    public Merchant(Guid id, string name, string phone, string address, Guid ownerId, Guid categoryId) : base(id)
    {
        Name = name;
        Phone = phone;
        Address = address;
        OwnerId = ownerId;
        CategoryId = categoryId;
    }
}
