using FoodDelivery.Coupons;
using FoodDelivery.Merchants;
using FoodDelivery.Notifications;
using FoodDelivery.Orders;
using FoodDelivery.Payments;
using FoodDelivery.Products;
using FoodDelivery.Reviews;
using FoodDelivery.Riders;
using FoodDelivery.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace FoodDelivery.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class FoodDeliveryDbContext : AbpDbContext<FoodDeliveryDbContext>,
    IAbpEfCoreDbContext
{
    public DbSet<Merchant> Merchants { get; set; }
    public DbSet<MerchantCategory> MerchantCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductSpec> ProductSpecs { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderStatusLog> OrderStatusLogs { get; set; }
    public DbSet<Rider> Riders { get; set; }
    public DbSet<PaymentOrder> PaymentOrders { get; set; }
    public DbSet<RefundOrder> RefundOrders { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<CouponTemplate> CouponTemplates { get; set; }
    public DbSet<UserCoupon> UserCoupons { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureTenantManagement();
        builder.ConfigureFoodDelivery();
    }
}
