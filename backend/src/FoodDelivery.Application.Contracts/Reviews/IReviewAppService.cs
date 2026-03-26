using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FoodDelivery.Reviews;

public interface IReviewAppService : IApplicationService
{
    Task<ReviewDto> CreateAsync(CreateReviewDto input);
    Task<PagedResultDto<ReviewDto>> GetMerchantReviewsAsync(Guid merchantId, PagedAndSortedResultRequestDto input);
    Task<ReviewDto> ReplyAsync(Guid id, string reply);
}
