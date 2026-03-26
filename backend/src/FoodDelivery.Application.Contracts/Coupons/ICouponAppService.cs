using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FoodDelivery.Coupons;

public interface ICouponAppService : IApplicationService
{
    Task<List<CouponTemplateDto>> GetAvailableCouponsAsync(Guid merchantId, decimal orderAmount);
    Task<PagedResultDto<CouponTemplateDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<CouponTemplateDto> CreateAsync(CouponTemplateDto input);
    Task ClaimCouponAsync(Guid couponTemplateId);
}
