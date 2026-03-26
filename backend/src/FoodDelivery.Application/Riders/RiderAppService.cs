using FoodDelivery.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FoodDelivery.Riders;

[Authorize]
public class RiderAppService : ApplicationService, IRiderAppService
{
    private readonly IRepository<Rider, Guid> _riderRepository;

    public RiderAppService(IRepository<Rider, Guid> riderRepository)
    {
        _riderRepository = riderRepository;
    }

    public async Task<RiderDto> RegisterAsync(CreateRiderDto input)
    {
        var rider = new Rider(
            GuidGenerator.Create(),
            CurrentUser.Id!.Value,
            input.RealName,
            input.Phone,
            input.IdCardNo);

        rider.IdCardFront = input.IdCardFront;
        rider.IdCardBack = input.IdCardBack;

        await _riderRepository.InsertAsync(rider);
        return ObjectMapper.Map<Rider, RiderDto>(rider);
    }

    public async Task<RiderDto> GetAsync(Guid id)
    {
        var rider = await _riderRepository.GetAsync(id);
        return ObjectMapper.Map<Rider, RiderDto>(rider);
    }

    public async Task<RiderDto> GetMyInfoAsync()
    {
        var rider = await _riderRepository.FirstOrDefaultAsync(r => r.UserId == CurrentUser.Id!.Value)
            ?? throw new Volo.Abp.UserFriendlyException("Rider not found");
        return ObjectMapper.Map<Rider, RiderDto>(rider);
    }

    public async Task<PagedResultDto<RiderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var total = await _riderRepository.CountAsync();
        var items = await _riderRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? "Id");

        return new PagedResultDto<RiderDto>(
            total,
            ObjectMapper.Map<List<Rider>, List<RiderDto>>(items));
    }

    [Authorize(FoodDeliveryPermissions.Riders.Audit)]
    public async Task<RiderDto> ApproveAsync(Guid id)
    {
        var rider = await _riderRepository.GetAsync(id);
        rider.GoOnline();
        await _riderRepository.UpdateAsync(rider);
        return ObjectMapper.Map<Rider, RiderDto>(rider);
    }

    [Authorize(FoodDeliveryPermissions.Riders.Audit)]
    public async Task<RiderDto> RejectAsync(Guid id, string reason)
    {
        var rider = await _riderRepository.GetAsync(id);
        rider.Status = FoodDelivery.Enums.RiderStatus.Banned;
        await _riderRepository.UpdateAsync(rider);
        return ObjectMapper.Map<Rider, RiderDto>(rider);
    }

    public async Task<RiderDto> UpdateLocationAsync(double lat, double lng)
    {
        var rider = await _riderRepository.FirstOrDefaultAsync(r => r.UserId == CurrentUser.Id!.Value)
            ?? throw new Volo.Abp.UserFriendlyException("Rider not found");
        rider.UpdateLocation(lat, lng);
        await _riderRepository.UpdateAsync(rider);
        return ObjectMapper.Map<Rider, RiderDto>(rider);
    }

    public async Task<RiderDto> GoOnlineAsync()
    {
        var rider = await _riderRepository.FirstOrDefaultAsync(r => r.UserId == CurrentUser.Id!.Value)
            ?? throw new Volo.Abp.UserFriendlyException("Rider not found");
        rider.GoOnline();
        await _riderRepository.UpdateAsync(rider);
        return ObjectMapper.Map<Rider, RiderDto>(rider);
    }

    public async Task<RiderDto> GoOfflineAsync()
    {
        var rider = await _riderRepository.FirstOrDefaultAsync(r => r.UserId == CurrentUser.Id!.Value)
            ?? throw new Volo.Abp.UserFriendlyException("Rider not found");
        rider.GoOffline();
        await _riderRepository.UpdateAsync(rider);
        return ObjectMapper.Map<Rider, RiderDto>(rider);
    }
}
