using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FoodDelivery.Products;

public interface IProductAppService : IApplicationService
{
    Task<ProductDto> GetAsync(Guid id);
    Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input);
    Task<ProductDto> CreateAsync(CreateUpdateProductDto input);
    Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input);
    Task DeleteAsync(Guid id);
    Task<ProductDto> SetAvailableAsync(Guid id, bool available);
    Task<List<ProductDto>> GetMerchantProductsAsync(Guid merchantId);
}
