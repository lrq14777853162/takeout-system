using FoodDelivery.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FoodDelivery.Orders;

[Authorize]
public class OrderAppService : ApplicationService, IOrderAppService
{
    private readonly IRepository<Order, Guid> _orderRepository;

    public OrderAppService(IRepository<Order, Guid> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto input)
    {
        var order = new Order(
            GuidGenerator.Create(),
            GenerateOrderNo(),
            CurrentUser.Id!.Value,
            input.MerchantId);

        await _orderRepository.InsertAsync(order);
        return ObjectMapper.Map<Order, OrderDto>(order);
    }

    public async Task<OrderDto> GetAsync(Guid id)
    {
        var order = await _orderRepository.GetAsync(id);
        return ObjectMapper.Map<Order, OrderDto>(order);
    }

    public async Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input)
    {
        var query = await _orderRepository.GetQueryableAsync();

        if (input.Status.HasValue)
            query = query.Where(o => o.Status == input.Status.Value);
        if (input.MerchantId.HasValue)
            query = query.Where(o => o.MerchantId == input.MerchantId.Value);
        if (input.CustomerId.HasValue)
            query = query.Where(o => o.CustomerId == input.CustomerId.Value);

        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        return new PagedResultDto<OrderDto>(
            total,
            ObjectMapper.Map<List<Order>, List<OrderDto>>(items));
    }

    [Authorize(FoodDeliveryPermissions.Orders.Accept)]
    public async Task<OrderDto> AcceptAsync(Guid id)
    {
        var order = await _orderRepository.GetAsync(id);
        order.Accept();
        await _orderRepository.UpdateAsync(order);
        return ObjectMapper.Map<Order, OrderDto>(order);
    }

    public async Task<OrderDto> AssignRiderAsync(Guid id, Guid riderId)
    {
        var order = await _orderRepository.GetAsync(id);
        order.AssignRider(riderId);
        await _orderRepository.UpdateAsync(order);
        return ObjectMapper.Map<Order, OrderDto>(order);
    }

    public async Task<OrderDto> PickUpAsync(Guid id)
    {
        var order = await _orderRepository.GetAsync(id);
        order.PickUp();
        await _orderRepository.UpdateAsync(order);
        return ObjectMapper.Map<Order, OrderDto>(order);
    }

    public async Task<OrderDto> DeliverAsync(Guid id)
    {
        var order = await _orderRepository.GetAsync(id);
        order.Deliver();
        await _orderRepository.UpdateAsync(order);
        return ObjectMapper.Map<Order, OrderDto>(order);
    }

    public async Task<OrderDto> CompleteAsync(Guid id)
    {
        var order = await _orderRepository.GetAsync(id);
        order.Complete();
        await _orderRepository.UpdateAsync(order);
        return ObjectMapper.Map<Order, OrderDto>(order);
    }

    [Authorize(FoodDeliveryPermissions.Orders.Cancel)]
    public async Task<OrderDto> CancelAsync(Guid id, string? reason = null)
    {
        var order = await _orderRepository.GetAsync(id);
        order.Cancel();
        await _orderRepository.UpdateAsync(order);
        return ObjectMapper.Map<Order, OrderDto>(order);
    }

    public async Task<PagedResultDto<OrderDto>> GetMerchantOrdersAsync(Guid merchantId, GetOrderListDto input)
    {
        input.MerchantId = merchantId;
        return await GetListAsync(input);
    }

    public async Task<PagedResultDto<OrderDto>> GetRiderOrdersAsync(Guid riderId, GetOrderListDto input)
    {
        input.RiderId = riderId;
        return await GetListAsync(input);
    }

    private static string GenerateOrderNo() =>
        $"FD{DateTime.UtcNow:yyyyMMddHHmmss}{Random.Shared.Next(1000, 9999)}";
}
