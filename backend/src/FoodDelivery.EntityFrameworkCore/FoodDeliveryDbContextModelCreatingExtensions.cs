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

namespace FoodDelivery.EntityFrameworkCore;

public static class FoodDeliveryDbContextModelCreatingExtensions
{
    public static void ConfigureFoodDelivery(this ModelBuilder builder)
    {
        var prefix = FoodDeliveryConsts.DbTablePrefix;
        var schema = FoodDeliveryConsts.DbSchema;

        builder.Entity<MerchantCategory>(b =>
        {
            b.ToTable($"{prefix}MerchantCategories", schema);
            b.Property(x => x.Name).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
            b.Property(x => x.Icon).HasMaxLength(FoodDeliveryConsts.MaxUrlLength);
        });

        builder.Entity<Merchant>(b =>
        {
            b.ToTable($"{prefix}Merchants", schema);
            b.Property(x => x.Name).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
            b.Property(x => x.Phone).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxPhoneLength);
            b.Property(x => x.Address).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxAddressLength);
            b.Property(x => x.Description).HasMaxLength(FoodDeliveryConsts.MaxDescriptionLength);
            b.Property(x => x.Logo).HasMaxLength(FoodDeliveryConsts.MaxUrlLength);
            b.Property(x => x.BusinessLicense).HasMaxLength(FoodDeliveryConsts.MaxUrlLength);
            b.Property(x => x.FoodLicense).HasMaxLength(FoodDeliveryConsts.MaxUrlLength);
            b.HasMany(x => x.Products).WithOne().HasForeignKey(p => p.MerchantId);
            b.HasMany(x => x.ProductCategories).WithOne().HasForeignKey(p => p.MerchantId);
        });

        builder.Entity<ProductCategory>(b =>
        {
            b.ToTable($"{prefix}ProductCategories", schema);
            b.Property(x => x.Name).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
        });

        builder.Entity<Product>(b =>
        {
            b.ToTable($"{prefix}Products", schema);
            b.Property(x => x.Name).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
            b.Property(x => x.Description).HasMaxLength(FoodDeliveryConsts.MaxDescriptionLength);
            b.Property(x => x.Image).HasMaxLength(FoodDeliveryConsts.MaxUrlLength);
            b.Property(x => x.Price).HasColumnType("decimal(18,2)");
            b.Property(x => x.OriginalPrice).HasColumnType("decimal(18,2)");
            b.Property(x => x.PackingFee).HasColumnType("decimal(18,2)");
            b.HasMany(x => x.Specs).WithOne().HasForeignKey(s => s.ProductId);
        });

        builder.Entity<ProductSpec>(b =>
        {
            b.ToTable($"{prefix}ProductSpecs", schema);
            b.Property(x => x.Name).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
            b.Property(x => x.PriceAdjustment).HasColumnType("decimal(18,2)");
        });

        builder.Entity<Order>(b =>
        {
            b.ToTable($"{prefix}Orders", schema);
            b.Property(x => x.OrderNo).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxOrderNoLength);
            b.Property(x => x.DeliveryAddress).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxAddressLength);
            b.Property(x => x.ContactName).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
            b.Property(x => x.ContactPhone).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxPhoneLength);
            b.Property(x => x.Remark).HasMaxLength(FoodDeliveryConsts.MaxRemarkLength);
            b.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.DeliveryFee).HasColumnType("decimal(18,2)");
            b.Property(x => x.PackingFee).HasColumnType("decimal(18,2)");
            b.Property(x => x.DiscountAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.ActualAmount).HasColumnType("decimal(18,2)");
            b.HasMany(x => x.Items).WithOne().HasForeignKey(i => i.OrderId);
            b.HasMany(x => x.StatusLogs).WithOne().HasForeignKey(l => l.OrderId);
        });

        builder.Entity<OrderItem>(b =>
        {
            b.ToTable($"{prefix}OrderItems", schema);
            b.Property(x => x.ProductName).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
            b.Property(x => x.ProductImage).HasMaxLength(FoodDeliveryConsts.MaxUrlLength);
            b.Property(x => x.Price).HasColumnType("decimal(18,2)");
            b.Property(x => x.SpecInfo).HasMaxLength(FoodDeliveryConsts.MaxJsonLength);
        });

        builder.Entity<OrderStatusLog>(b =>
        {
            b.ToTable($"{prefix}OrderStatusLogs", schema);
            b.Property(x => x.OperatorRole).HasMaxLength(64);
            b.Property(x => x.Remark).HasMaxLength(FoodDeliveryConsts.MaxRemarkLength);
        });

        builder.Entity<Rider>(b =>
        {
            b.ToTable($"{prefix}Riders", schema);
            b.Property(x => x.RealName).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
            b.Property(x => x.Phone).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxPhoneLength);
            b.Property(x => x.IdCardNo).IsRequired().HasMaxLength(32);
            b.Property(x => x.IdCardFront).HasMaxLength(FoodDeliveryConsts.MaxUrlLength);
            b.Property(x => x.IdCardBack).HasMaxLength(FoodDeliveryConsts.MaxUrlLength);
            b.Property(x => x.TotalEarnings).HasColumnType("decimal(18,2)");
            b.Property(x => x.Balance).HasColumnType("decimal(18,2)");
        });

        builder.Entity<PaymentOrder>(b =>
        {
            b.ToTable($"{prefix}PaymentOrders", schema);
            b.Property(x => x.OrderNo).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxOrderNoLength);
            b.Property(x => x.TransactionId).HasMaxLength(128);
            b.Property(x => x.Amount).HasColumnType("decimal(18,2)");
        });

        builder.Entity<RefundOrder>(b =>
        {
            b.ToTable($"{prefix}RefundOrders", schema);
            b.Property(x => x.Reason).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxDescriptionLength);
            b.Property(x => x.RefundAmount).HasColumnType("decimal(18,2)");
        });

        builder.Entity<Review>(b =>
        {
            b.ToTable($"{prefix}Reviews", schema);
            b.Property(x => x.Content).HasMaxLength(FoodDeliveryConsts.MaxDescriptionLength);
            b.Property(x => x.Images).HasMaxLength(FoodDeliveryConsts.MaxJsonLength);
            b.Property(x => x.MerchantReply).HasMaxLength(FoodDeliveryConsts.MaxDescriptionLength);
        });

        builder.Entity<CouponTemplate>(b =>
        {
            b.ToTable($"{prefix}CouponTemplates", schema);
            b.Property(x => x.Name).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
            b.Property(x => x.Value).HasColumnType("decimal(18,2)");
            b.Property(x => x.MinOrderAmount).HasColumnType("decimal(18,2)");
        });

        builder.Entity<UserCoupon>(b =>
        {
            b.ToTable($"{prefix}UserCoupons", schema);
        });

        builder.Entity<UserAddress>(b =>
        {
            b.ToTable($"{prefix}UserAddresses", schema);
            b.Property(x => x.ContactName).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
            b.Property(x => x.ContactPhone).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxPhoneLength);
            b.Property(x => x.Province).IsRequired().HasMaxLength(64);
            b.Property(x => x.City).IsRequired().HasMaxLength(64);
            b.Property(x => x.District).IsRequired().HasMaxLength(64);
            b.Property(x => x.DetailAddress).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxAddressLength);
            b.Property(x => x.Tag).HasMaxLength(32);
        });

        builder.Entity<Notification>(b =>
        {
            b.ToTable($"{prefix}Notifications", schema);
            b.Property(x => x.Title).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxNameLength);
            b.Property(x => x.Content).IsRequired().HasMaxLength(FoodDeliveryConsts.MaxDescriptionLength);
        });
    }
}
