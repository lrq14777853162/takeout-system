namespace FoodDelivery.Enums;

public enum RiderStatus
{
    Pending = 0,    // 审核中
    Online = 1,     // 在线空闲
    Delivering = 2, // 配送中
    Offline = 3,    // 离线
    Banned = 4      // 已封禁
}
