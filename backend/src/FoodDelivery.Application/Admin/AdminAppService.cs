using FoodDelivery.Merchants;
using FoodDelivery.Orders;
using FoodDelivery.Riders;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FoodDelivery.Admin;

[Authorize]
public class AdminAppService : ApplicationService, IAdminAppService
{
    private readonly IRepository<Order, Guid> _orderRepository;
    private readonly IRepository<Merchant, Guid> _merchantRepository;
    private readonly IRepository<Rider, Guid> _riderRepository;

    public AdminAppService(
        IRepository<Order, Guid> orderRepository,
        IRepository<Merchant, Guid> merchantRepository,
        IRepository<Rider, Guid> riderRepository)
    {
        _orderRepository = orderRepository;
        _merchantRepository = merchantRepository;
        _riderRepository = riderRepository;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        var today = DateTime.UtcNow.Date;

        var todayOrders = await _orderRepository.CountAsync(o => o.CreationTime >= today);
        var todayRevenue = (await _orderRepository.GetListAsync(o => o.CreationTime >= today && o.Status == Enums.OrderStatus.Completed)).Sum(o => o.ActualAmount);
        var pendingMerchants = await _merchantRepository.CountAsync(m => m.Status == Enums.MerchantStatus.Pending);
        var pendingRiders = await _riderRepository.CountAsync(r => r.Status == Enums.RiderStatus.Pending);
        var activeRiders = await _riderRepository.CountAsync(r => r.Status == Enums.RiderStatus.Online || r.Status == Enums.RiderStatus.Delivering);
        var totalMerchants = await _merchantRepository.CountAsync();
        var totalOrders = await _orderRepository.CountAsync();

        return new DashboardDto
        {
            TodayOrders = todayOrders,
            TodayRevenue = todayRevenue,
            PendingAuditMerchants = pendingMerchants,
            PendingAuditRiders = pendingRiders,
            ActiveRiders = activeRiders,
            TotalMerchants = totalMerchants,
            TotalOrders = totalOrders
        };
    }
}
