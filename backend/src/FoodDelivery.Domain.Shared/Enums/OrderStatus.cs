namespace FoodDelivery.Enums;

public enum OrderStatus
{
    Pending = 0,        // 待支付
    Paid = 1,           // 已支付
    Accepted = 2,       // 商家已接单
    ReadyForPickup = 3, // 待骑手取餐
    PickingUp = 4,      // 骑手前往取餐
    Delivering = 5,     // 配送中
    Delivered = 6,      // 已送达
    Completed = 7,      // 已完成
    Cancelled = 8,      // 已取消
    Refunding = 9,      // 退款中
    Refunded = 10       // 已退款
}
