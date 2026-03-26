using Volo.Abp.Application.Services;

namespace FoodDelivery.Payments;

public interface IPaymentAppService : IApplicationService
{
    Task<PaymentOrderDto> CreatePaymentAsync(CreatePaymentDto input);
    Task<PaymentOrderDto> HandleCallbackAsync(string orderNo, string transactionId, bool success);
    Task<PaymentOrderDto> RequestRefundAsync(Guid orderId, decimal amount, string reason);
}
