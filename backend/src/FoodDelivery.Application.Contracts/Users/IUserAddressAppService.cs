using Volo.Abp.Application.Services;

namespace FoodDelivery.Users;

public interface IUserAddressAppService : IApplicationService
{
    Task<List<UserAddressDto>> GetMyAddressesAsync();
    Task<UserAddressDto> CreateAsync(CreateUpdateUserAddressDto input);
    Task<UserAddressDto> UpdateAsync(Guid id, CreateUpdateUserAddressDto input);
    Task DeleteAsync(Guid id);
    Task<UserAddressDto> SetDefaultAsync(Guid id);
}
