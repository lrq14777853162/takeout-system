using FoodDelivery.Enums;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FoodDelivery.Coupons;

[Authorize]
public class CouponAppService : ApplicationService, ICouponAppService
{
    private readonly IRepository<CouponTemplate, Guid> _templateRepository;
    private readonly IRepository<UserCoupon, Guid> _userCouponRepository;

    public CouponAppService(
        IRepository<CouponTemplate, Guid> templateRepository,
        IRepository<UserCoupon, Guid> userCouponRepository)
    {
        _templateRepository = templateRepository;
        _userCouponRepository = userCouponRepository;
    }

    public async Task<List<CouponTemplateDto>> GetAvailableCouponsAsync(Guid merchantId, decimal orderAmount)
    {
        var now = DateTime.UtcNow;
        var templates = await _templateRepository.GetListAsync(t =>
            t.RemainCount > 0 &&
            t.StartTime <= now &&
            t.EndTime >= now &&
            t.MinOrderAmount <= orderAmount &&
            (t.Scope == 0 || t.ScopeMerchantId == merchantId));

        return ObjectMapper.Map<List<CouponTemplate>, List<CouponTemplateDto>>(templates);
    }

    public async Task<PagedResultDto<CouponTemplateDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var total = await _templateRepository.CountAsync();
        var items = await _templateRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? "Id");

        return new PagedResultDto<CouponTemplateDto>(
            total,
            ObjectMapper.Map<List<CouponTemplate>, List<CouponTemplateDto>>(items));
    }

    public async Task<CouponTemplateDto> CreateAsync(CouponTemplateDto input)
    {
        var template = new CouponTemplate(
            GuidGenerator.Create(),
            input.Name,
            input.Type,
            input.Value,
            input.MinOrderAmount);

        template.StartTime = input.StartTime;
        template.EndTime = input.EndTime;
        template.TotalCount = input.TotalCount;
        template.RemainCount = input.TotalCount;

        await _templateRepository.InsertAsync(template);
        return ObjectMapper.Map<CouponTemplate, CouponTemplateDto>(template);
    }

    public async Task ClaimCouponAsync(Guid couponTemplateId)
    {
        var template = await _templateRepository.GetAsync(couponTemplateId);
        if (template.RemainCount <= 0)
            throw new Volo.Abp.UserFriendlyException("Coupon exhausted");

        var userCoupon = new UserCoupon(GuidGenerator.Create(), CurrentUser.Id!.Value, couponTemplateId);
        template.RemainCount--;

        await _userCouponRepository.InsertAsync(userCoupon);
        await _templateRepository.UpdateAsync(template);
    }
}
