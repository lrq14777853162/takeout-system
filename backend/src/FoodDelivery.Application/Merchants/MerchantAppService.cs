using FoodDelivery.Enums;
using FoodDelivery.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FoodDelivery.Merchants;

[Authorize]
public class MerchantAppService : ApplicationService, IMerchantAppService
{
    private readonly IRepository<Merchant, Guid> _merchantRepository;

    public MerchantAppService(IRepository<Merchant, Guid> merchantRepository)
    {
        _merchantRepository = merchantRepository;
    }

    public async Task<MerchantDto> GetAsync(Guid id)
    {
        var merchant = await _merchantRepository.GetAsync(id);
        return ObjectMapper.Map<Merchant, MerchantDto>(merchant);
    }

    public async Task<PagedResultDto<MerchantDto>> GetListAsync(GetMerchantListDto input)
    {
        var query = await _merchantRepository.GetQueryableAsync();

        if (input.Status.HasValue)
            query = query.Where(m => m.Status == input.Status.Value);
        if (input.CategoryId.HasValue)
            query = query.Where(m => m.CategoryId == input.CategoryId.Value);
        if (!string.IsNullOrEmpty(input.Keyword))
            query = query.Where(m => m.Name.Contains(input.Keyword));

        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        return new PagedResultDto<MerchantDto>(
            total,
            ObjectMapper.Map<List<Merchant>, List<MerchantDto>>(items));
    }

    public async Task<MerchantDto> CreateAsync(CreateUpdateMerchantDto input)
    {
        var merchant = new Merchant(
            GuidGenerator.Create(),
            input.Name,
            input.Phone,
            input.Address,
            CurrentUser.Id!.Value,
            input.CategoryId);

        ObjectMapper.Map(input, merchant);
        await _merchantRepository.InsertAsync(merchant);
        return ObjectMapper.Map<Merchant, MerchantDto>(merchant);
    }

    public async Task<MerchantDto> UpdateAsync(Guid id, CreateUpdateMerchantDto input)
    {
        var merchant = await _merchantRepository.GetAsync(id);
        ObjectMapper.Map(input, merchant);
        await _merchantRepository.UpdateAsync(merchant);
        return ObjectMapper.Map<Merchant, MerchantDto>(merchant);
    }

    [Authorize(FoodDeliveryPermissions.Merchants.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _merchantRepository.DeleteAsync(id);
    }

    [Authorize(FoodDeliveryPermissions.Merchants.Audit)]
    public async Task<MerchantDto> ApproveAsync(Guid id)
    {
        var merchant = await _merchantRepository.GetAsync(id);
        merchant.Status = MerchantStatus.Open;
        await _merchantRepository.UpdateAsync(merchant);
        return ObjectMapper.Map<Merchant, MerchantDto>(merchant);
    }

    [Authorize(FoodDeliveryPermissions.Merchants.Audit)]
    public async Task<MerchantDto> RejectAsync(Guid id, string reason)
    {
        var merchant = await _merchantRepository.GetAsync(id);
        merchant.Status = MerchantStatus.Disabled;
        await _merchantRepository.UpdateAsync(merchant);
        return ObjectMapper.Map<Merchant, MerchantDto>(merchant);
    }

    public async Task<MerchantDto> GetMyMerchantAsync()
    {
        var merchant = await _merchantRepository.FirstOrDefaultAsync(m => m.OwnerId == CurrentUser.Id!.Value)
            ?? throw new Volo.Abp.UserFriendlyException("No merchant found for current user");
        return ObjectMapper.Map<Merchant, MerchantDto>(merchant);
    }
}
