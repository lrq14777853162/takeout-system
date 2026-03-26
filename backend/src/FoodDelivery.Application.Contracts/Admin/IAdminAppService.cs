using Volo.Abp.Application.Services;

namespace FoodDelivery.Admin;

public interface IAdminAppService : IApplicationService
{
    Task<DashboardDto> GetDashboardAsync();
}
