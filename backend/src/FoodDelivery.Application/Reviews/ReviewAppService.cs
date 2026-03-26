using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FoodDelivery.Reviews;

[Authorize]
public class ReviewAppService : ApplicationService, IReviewAppService
{
    private readonly IRepository<Review, Guid> _reviewRepository;

    public ReviewAppService(IRepository<Review, Guid> reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<ReviewDto> CreateAsync(CreateReviewDto input)
    {
        var review = new Review(
            GuidGenerator.Create(),
            input.OrderId,
            CurrentUser.Id!.Value,
            Guid.Empty, // Will be filled from order
            input.MerchantRating);

        review.RiderRating = input.RiderRating;
        review.Content = input.Content;
        review.Images = input.Images.Count > 0 ? System.Text.Json.JsonSerializer.Serialize(input.Images) : null;

        await _reviewRepository.InsertAsync(review);
        return ObjectMapper.Map<Review, ReviewDto>(review);
    }

    public async Task<PagedResultDto<ReviewDto>> GetMerchantReviewsAsync(Guid merchantId, PagedAndSortedResultRequestDto input)
    {
        var query = await _reviewRepository.GetQueryableAsync();
        query = query.Where(r => r.MerchantId == merchantId);

        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        return new PagedResultDto<ReviewDto>(
            total,
            ObjectMapper.Map<List<Review>, List<ReviewDto>>(items));
    }

    public async Task<ReviewDto> ReplyAsync(Guid id, string reply)
    {
        var review = await _reviewRepository.GetAsync(id);
        review.MerchantReply = reply;
        review.MerchantReplyTime = DateTime.UtcNow;
        await _reviewRepository.UpdateAsync(review);
        return ObjectMapper.Map<Review, ReviewDto>(review);
    }
}
