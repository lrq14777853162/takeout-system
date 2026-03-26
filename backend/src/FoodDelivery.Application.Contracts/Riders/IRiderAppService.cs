using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FoodDelivery.Riders;

public interface IRiderAppService : IApplicationService
{
    Task<RiderDto> RegisterAsync(CreateRiderDto input);
    Task<RiderDto> GetAsync(Guid id);
    Task<RiderDto> GetMyInfoAsync();
    Task<PagedResultDto<RiderDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<RiderDto> ApproveAsync(Guid id);
    Task<RiderDto> RejectAsync(Guid id, string reason);
    Task<RiderDto> UpdateLocationAsync(double lat, double lng);
    Task<RiderDto> GoOnlineAsync();
    Task<RiderDto> GoOfflineAsync();
}
