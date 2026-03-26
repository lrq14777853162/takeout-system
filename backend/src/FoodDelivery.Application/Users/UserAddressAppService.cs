using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FoodDelivery.Users;

[Authorize]
public class UserAddressAppService : ApplicationService, IUserAddressAppService
{
    private readonly IRepository<UserAddress, Guid> _addressRepository;

    public UserAddressAppService(IRepository<UserAddress, Guid> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<List<UserAddressDto>> GetMyAddressesAsync()
    {
        var addresses = await _addressRepository.GetListAsync(a => a.UserId == CurrentUser.Id!.Value);
        return ObjectMapper.Map<List<UserAddress>, List<UserAddressDto>>(addresses);
    }

    public async Task<UserAddressDto> CreateAsync(CreateUpdateUserAddressDto input)
    {
        var address = new UserAddress(
            GuidGenerator.Create(),
            CurrentUser.Id!.Value,
            input.ContactName,
            input.ContactPhone,
            input.DetailAddress);

        ObjectMapper.Map(input, address);
        address.UserId = CurrentUser.Id!.Value;

        if (input.IsDefault)
        {
            var existing = await _addressRepository.GetListAsync(a => a.UserId == CurrentUser.Id!.Value && a.IsDefault);
            foreach (var a in existing) { a.IsDefault = false; await _addressRepository.UpdateAsync(a); }
        }

        await _addressRepository.InsertAsync(address);
        return ObjectMapper.Map<UserAddress, UserAddressDto>(address);
    }

    public async Task<UserAddressDto> UpdateAsync(Guid id, CreateUpdateUserAddressDto input)
    {
        var address = await _addressRepository.GetAsync(id);
        ObjectMapper.Map(input, address);
        await _addressRepository.UpdateAsync(address);
        return ObjectMapper.Map<UserAddress, UserAddressDto>(address);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _addressRepository.DeleteAsync(id);
    }

    public async Task<UserAddressDto> SetDefaultAsync(Guid id)
    {
        var existing = await _addressRepository.GetListAsync(a => a.UserId == CurrentUser.Id!.Value && a.IsDefault);
        foreach (var a in existing) { a.IsDefault = false; await _addressRepository.UpdateAsync(a); }

        var address = await _addressRepository.GetAsync(id);
        address.IsDefault = true;
        await _addressRepository.UpdateAsync(address);
        return ObjectMapper.Map<UserAddress, UserAddressDto>(address);
    }
}
