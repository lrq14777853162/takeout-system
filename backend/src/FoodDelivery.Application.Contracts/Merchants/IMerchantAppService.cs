using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FoodDelivery.Merchants;

public interface IMerchantAppService : IApplicationService
{
    Task<MerchantDto> GetAsync(Guid id);
    Task<PagedResultDto<MerchantDto>> GetListAsync(GetMerchantListDto input);
    Task<MerchantDto> CreateAsync(CreateUpdateMerchantDto input);
    Task<MerchantDto> UpdateAsync(Guid id, CreateUpdateMerchantDto input);
    Task DeleteAsync(Guid id);
    Task<MerchantDto> ApproveAsync(Guid id);
    Task<MerchantDto> RejectAsync(Guid id, string reason);
    Task<MerchantDto> GetMyMerchantAsync();
}
