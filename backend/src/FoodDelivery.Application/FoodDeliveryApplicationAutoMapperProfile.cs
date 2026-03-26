using AutoMapper;
using FoodDelivery.Coupons;
using FoodDelivery.Merchants;
using FoodDelivery.Orders;
using FoodDelivery.Payments;
using FoodDelivery.Products;
using FoodDelivery.Reviews;
using FoodDelivery.Riders;
using FoodDelivery.Users;

namespace FoodDelivery;

public class FoodDeliveryApplicationAutoMapperProfile : Profile
{
    public FoodDeliveryApplicationAutoMapperProfile()
    {
        // Orders
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderStatusLog, OrderStatusLogDto>();

        // Merchants
        CreateMap<Merchant, MerchantDto>();
        CreateMap<CreateUpdateMerchantDto, Merchant>().IgnoreAuditedObjectProperties().Ignore(x => x.Id).Ignore(x => x.Status).Ignore(x => x.Rating).Ignore(x => x.MonthlySales).Ignore(x => x.Products).Ignore(x => x.ProductCategories);

        // Products
        CreateMap<Product, ProductDto>();
        CreateMap<ProductSpec, ProductSpecDto>();
        CreateMap<CreateUpdateProductDto, Product>().IgnoreAuditedObjectProperties().Ignore(x => x.Id).Ignore(x => x.MonthlySales).Ignore(x => x.Specs);

        // Riders
        CreateMap<Rider, RiderDto>();

        // Payments
        CreateMap<PaymentOrder, PaymentOrderDto>();

        // Reviews
        CreateMap<Review, ReviewDto>();

        // Users
        CreateMap<UserAddress, UserAddressDto>();
        CreateMap<CreateUpdateUserAddressDto, UserAddress>().IgnoreAuditedObjectProperties().Ignore(x => x.Id).Ignore(x => x.UserId);

        // Coupons
        CreateMap<CouponTemplate, CouponTemplateDto>();
    }
}
