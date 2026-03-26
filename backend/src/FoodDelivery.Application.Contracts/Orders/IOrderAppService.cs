using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FoodDelivery.Orders;

public interface IOrderAppService : IApplicationService
{
    Task<OrderDto> CreateAsync(CreateOrderDto input);
    Task<OrderDto> GetAsync(Guid id);
    Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input);
    Task<OrderDto> AcceptAsync(Guid id);
    Task<OrderDto> AssignRiderAsync(Guid id, Guid riderId);
    Task<OrderDto> PickUpAsync(Guid id);
    Task<OrderDto> DeliverAsync(Guid id);
    Task<OrderDto> CompleteAsync(Guid id);
    Task<OrderDto> CancelAsync(Guid id, string? reason = null);
    Task<PagedResultDto<OrderDto>> GetMerchantOrdersAsync(Guid merchantId, GetOrderListDto input);
    Task<PagedResultDto<OrderDto>> GetRiderOrdersAsync(Guid riderId, GetOrderListDto input);
}
