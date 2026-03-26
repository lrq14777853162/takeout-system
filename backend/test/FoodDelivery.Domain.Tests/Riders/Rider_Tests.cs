using FoodDelivery.Riders;
using FoodDelivery.Enums;
using Shouldly;
using Xunit;

namespace FoodDelivery.Domain.Tests.Riders;

public class Rider_Tests
{
    [Fact]
    public void Should_Update_Location()
    {
        var rider = new Rider(Guid.NewGuid(), Guid.NewGuid(), "Test Rider", "13800138000", "110101199001011234");

        rider.UpdateLocation(39.9042, 116.4074);

        rider.CurrentLat.ShouldBe(39.9042);
        rider.CurrentLng.ShouldBe(116.4074);
        rider.LastLocationTime.ShouldNotBeNull();
    }

    [Fact]
    public void Should_Go_Online_And_Offline()
    {
        var rider = new Rider(Guid.NewGuid(), Guid.NewGuid(), "Test Rider", "13800138000", "110101199001011234");

        rider.GoOnline();
        rider.Status.ShouldBe(RiderStatus.Online);

        rider.GoOffline();
        rider.Status.ShouldBe(RiderStatus.Offline);
    }
}
