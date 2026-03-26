using FoodDelivery.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Orders;

/// <summary>订单聚合根</summary>
public class Order : FullAuditedAggregateRoot<Guid>
{
    public string OrderNo { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public Guid MerchantId { get; set; }
    public Guid? RiderId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal PackingFee { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal ActualAmount { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public double DeliveryLat { get; set; }
    public double DeliveryLng { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string? Remark { get; set; }
    public DateTime? PaidTime { get; set; }
    public DateTime? AcceptedTime { get; set; }
    public DateTime? PickedUpTime { get; set; }
    public DateTime? DeliveredTime { get; set; }
    public DateTime? CancelledTime { get; set; }
    public DateTime? ExpectedDeliveryTime { get; set; }

    public virtual ICollection<OrderItem> Items { get; set; } = [];
    public virtual ICollection<OrderStatusLog> StatusLogs { get; set; } = [];

    protected Order() { }

    public Order(Guid id, string orderNo, Guid customerId, Guid merchantId) : base(id)
    {
        OrderNo = orderNo;
        CustomerId = customerId;
        MerchantId = merchantId;
    }

    /// <summary>商家接单</summary>
    public void Accept()
    {
        if (Status != OrderStatus.Paid)
            throw new BusinessException(FoodDeliveryDomainErrorCodes.OrderStatusInvalid);
        Status = OrderStatus.Accepted;
        AcceptedTime = DateTime.UtcNow;
        ExpectedDeliveryTime = DateTime.UtcNow.AddMinutes(45);
    }

    /// <summary>分配骑手</summary>
    public void AssignRider(Guid riderId)
    {
        if (Status != OrderStatus.Accepted)
            throw new BusinessException(FoodDeliveryDomainErrorCodes.OrderStatusInvalid);
        RiderId = riderId;
        Status = OrderStatus.ReadyForPickup;
    }

    /// <summary>骑手取餐</summary>
    public void PickUp()
    {
        if (Status != OrderStatus.ReadyForPickup && Status != OrderStatus.PickingUp)
            throw new BusinessException(FoodDeliveryDomainErrorCodes.OrderStatusInvalid);
        Status = OrderStatus.Delivering;
        PickedUpTime = DateTime.UtcNow;
    }

    /// <summary>送达</summary>
    public void Deliver()
    {
        if (Status != OrderStatus.Delivering)
            throw new BusinessException(FoodDeliveryDomainErrorCodes.OrderStatusInvalid);
        Status = OrderStatus.Delivered;
        DeliveredTime = DateTime.UtcNow;
    }

    /// <summary>完成</summary>
    public void Complete()
    {
        if (Status != OrderStatus.Delivered)
            throw new BusinessException(FoodDeliveryDomainErrorCodes.OrderStatusInvalid);
        Status = OrderStatus.Completed;
    }

    /// <summary>取消</summary>
    public void Cancel()
    {
        if (Status is OrderStatus.Delivering or OrderStatus.Delivered or OrderStatus.Completed)
            throw new BusinessException(FoodDeliveryDomainErrorCodes.OrderStatusInvalid);
        Status = OrderStatus.Cancelled;
        CancelledTime = DateTime.UtcNow;
    }
}
