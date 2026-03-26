namespace FoodDelivery.Admin;

public class DashboardDto
{
    public int TodayOrders { get; set; }
    public decimal TodayRevenue { get; set; }
    public int PendingAuditMerchants { get; set; }
    public int PendingAuditRiders { get; set; }
    public int ActiveRiders { get; set; }
    public int TotalUsers { get; set; }
    public int TotalMerchants { get; set; }
    public int TotalOrders { get; set; }
    public List<DailyStatDto> WeeklyOrders { get; set; } = [];
    public List<DailyStatDto> WeeklyRevenue { get; set; } = [];
}

public class DailyStatDto
{
    public string Date { get; set; } = string.Empty;
    public decimal Value { get; set; }
}
