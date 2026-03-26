using FoodDelivery.Enums;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FoodDelivery.Payments;

[Authorize]
public class PaymentAppService : ApplicationService, IPaymentAppService
{
    private readonly IRepository<PaymentOrder, Guid> _paymentRepository;
    private readonly IRepository<Orders.Order, Guid> _orderRepository;

    public PaymentAppService(
        IRepository<PaymentOrder, Guid> paymentRepository,
        IRepository<Orders.Order, Guid> orderRepository)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
    }

    public async Task<PaymentOrderDto> CreatePaymentAsync(CreatePaymentDto input)
    {
        var order = await _orderRepository.GetAsync(input.OrderId);
        var payment = new PaymentOrder(
            GuidGenerator.Create(),
            order.Id,
            order.OrderNo,
            input.PaymentMethod,
            order.ActualAmount);

        await _paymentRepository.InsertAsync(payment);
        return ObjectMapper.Map<PaymentOrder, PaymentOrderDto>(payment);
    }

    public async Task<PaymentOrderDto> HandleCallbackAsync(string orderNo, string transactionId, bool success)
    {
        var payment = await _paymentRepository.FirstOrDefaultAsync(p => p.OrderNo == orderNo)
            ?? throw new Volo.Abp.UserFriendlyException("Payment not found");

        if (success)
        {
            payment.MarkSuccess(transactionId);
            var order = await _orderRepository.GetAsync(payment.OrderId);
            order.Status = OrderStatus.Paid;
            order.PaidTime = DateTime.UtcNow;
            await _orderRepository.UpdateAsync(order);
        }
        else
        {
            payment.MarkFailed();
        }

        await _paymentRepository.UpdateAsync(payment);
        return ObjectMapper.Map<PaymentOrder, PaymentOrderDto>(payment);
    }

    public async Task<PaymentOrderDto> RequestRefundAsync(Guid orderId, decimal amount, string reason)
    {
        var payment = await _paymentRepository.FirstOrDefaultAsync(p => p.OrderId == orderId)
            ?? throw new Volo.Abp.UserFriendlyException("Payment not found");

        payment.MarkRefunded();
        await _paymentRepository.UpdateAsync(payment);
        return ObjectMapper.Map<PaymentOrder, PaymentOrderDto>(payment);
    }
}
