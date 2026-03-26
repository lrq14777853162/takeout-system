namespace FoodDelivery.Enums;

public enum MerchantStatus
{
    Pending = 0,   // 审核中
    Open = 1,      // 营业中
    Closed = 2,    // 休息中
    Disabled = 3,  // 已关闭
    Banned = 4     // 已封禁
}
