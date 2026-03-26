using FoodDelivery.Orders;
using FoodDelivery.Enums;
using Shouldly;
using Xunit;

namespace FoodDelivery.Domain.Tests.Orders;

public class Order_Tests
{
    [Fact]
    public void Should_Accept_Order_When_Paid()
    {
        var order = new Order(Guid.NewGuid(), "ORD-001", Guid.NewGuid(), Guid.NewGuid());
        order.Status = OrderStatus.Paid;

        order.Accept();

        order.Status.ShouldBe(OrderStatus.Accepted);
        order.AcceptedTime.ShouldNotBeNull();
        order.ExpectedDeliveryTime.ShouldNotBeNull();
    }

    [Fact]
    public void Should_Throw_When_Accepting_Non_Paid_Order()
    {
        var order = new Order(Guid.NewGuid(), "ORD-002", Guid.NewGuid(), Guid.NewGuid());
        order.Status = OrderStatus.Pending;

        Should.Throw<Volo.Abp.BusinessException>(() => order.Accept());
    }

    [Fact]
    public void Should_Cancel_Order_When_Not_In_Delivery()
    {
        var order = new Order(Guid.NewGuid(), "ORD-003", Guid.NewGuid(), Guid.NewGuid());
        order.Status = OrderStatus.Paid;

        order.Cancel();

        order.Status.ShouldBe(OrderStatus.Cancelled);
        order.CancelledTime.ShouldNotBeNull();
    }

    [Fact]
    public void Should_Throw_When_Cancelling_Delivered_Order()
    {
        var order = new Order(Guid.NewGuid(), "ORD-004", Guid.NewGuid(), Guid.NewGuid());
        order.Status = OrderStatus.Delivered;

        Should.Throw<Volo.Abp.BusinessException>(() => order.Cancel());
    }
}
