using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodDelivery.EntityFrameworkCore;

public class FoodDeliveryDbContextFactory : IDesignTimeDbContextFactory<FoodDeliveryDbContext>
{
    public FoodDeliveryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FoodDeliveryDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=FoodDelivery;Trusted_Connection=True;TrustServerCertificate=True;");
        return new FoodDeliveryDbContext(optionsBuilder.Options);
    }
}
