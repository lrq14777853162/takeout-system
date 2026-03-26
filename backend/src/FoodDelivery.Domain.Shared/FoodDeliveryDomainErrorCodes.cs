namespace FoodDelivery;

public static class FoodDeliveryDomainErrorCodes
{
    public const string OrderNotFound = "FoodDelivery:OrderNotFound";
    public const string OrderStatusInvalid = "FoodDelivery:OrderStatusInvalid";
    public const string MerchantNotFound = "FoodDelivery:MerchantNotFound";
    public const string MerchantNotOpen = "FoodDelivery:MerchantNotOpen";
    public const string ProductNotFound = "FoodDelivery:ProductNotFound";
    public const string ProductUnavailable = "FoodDelivery:ProductUnavailable";
    public const string RiderNotFound = "FoodDelivery:RiderNotFound";
    public const string InsufficientStock = "FoodDelivery:InsufficientStock";
    public const string CouponNotFound = "FoodDelivery:CouponNotFound";
    public const string CouponUnavailable = "FoodDelivery:CouponUnavailable";
    public const string AddressNotFound = "FoodDelivery:AddressNotFound";
    public const string PaymentFailed = "FoodDelivery:PaymentFailed";
}
