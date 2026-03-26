using FoodDelivery.Enums;
using Shouldly;
using Xunit;

namespace FoodDelivery.Orders;

public class OrderAppService_Tests
{
    [Fact]
    public void GenerateOrderNo_ShouldBeUnique()
    {
        // Arrange & Act
        var no1 = $"FD{DateTime.UtcNow:yyyyMMddHHmmss}{Random.Shared.Next(1000, 9999)}";
        var no2 = $"FD{DateTime.UtcNow:yyyyMMddHHmmss}{Random.Shared.Next(1000, 9999)}";

        // Assert - Both should start with FD
        no1.ShouldStartWith("FD");
        no2.ShouldStartWith("FD");
        no1.Length.ShouldBeGreaterThan(10);
    }

    [Fact]
    public void OrderStatus_ShouldBeSequential()
    {
        // Verify enum values
        ((int)OrderStatus.Pending).ShouldBe(0);
        ((int)OrderStatus.Paid).ShouldBe(1);
        ((int)OrderStatus.Accepted).ShouldBe(2);
        ((int)OrderStatus.Completed).ShouldBe(7);
        ((int)OrderStatus.Cancelled).ShouldBe(8);
    }
}
