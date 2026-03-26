using FoodDelivery.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FoodDelivery.Products;

[Authorize]
public class ProductAppService : ApplicationService, IProductAppService
{
    private readonly IRepository<Product, Guid> _productRepository;

    public ProductAppService(IRepository<Product, Guid> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> GetAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(id);
        return ObjectMapper.Map<Product, ProductDto>(product);
    }

    public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
    {
        var query = await _productRepository.GetQueryableAsync();

        if (input.MerchantId.HasValue)
            query = query.Where(p => p.MerchantId == input.MerchantId.Value);
        if (input.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == input.CategoryId.Value);
        if (input.IsAvailable.HasValue)
            query = query.Where(p => p.IsAvailable == input.IsAvailable.Value);
        if (!string.IsNullOrEmpty(input.Keyword))
            query = query.Where(p => p.Name.Contains(input.Keyword));

        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        return new PagedResultDto<ProductDto>(
            total,
            ObjectMapper.Map<List<Product>, List<ProductDto>>(items));
    }

    [Authorize(FoodDeliveryPermissions.Products.Create)]
    public async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
    {
        var product = new Product(
            GuidGenerator.Create(),
            input.MerchantId,
            input.CategoryId,
            input.Name,
            input.Price);

        ObjectMapper.Map(input, product);
        await _productRepository.InsertAsync(product);
        return ObjectMapper.Map<Product, ProductDto>(product);
    }

    [Authorize(FoodDeliveryPermissions.Products.Edit)]
    public async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
    {
        var product = await _productRepository.GetAsync(id);
        ObjectMapper.Map(input, product);
        await _productRepository.UpdateAsync(product);
        return ObjectMapper.Map<Product, ProductDto>(product);
    }

    [Authorize(FoodDeliveryPermissions.Products.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public async Task<ProductDto> SetAvailableAsync(Guid id, bool available)
    {
        var product = await _productRepository.GetAsync(id);
        product.IsAvailable = available;
        await _productRepository.UpdateAsync(product);
        return ObjectMapper.Map<Product, ProductDto>(product);
    }

    public async Task<List<ProductDto>> GetMerchantProductsAsync(Guid merchantId)
    {
        var products = await _productRepository.GetListAsync(p => p.MerchantId == merchantId);
        return ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
    }
}
