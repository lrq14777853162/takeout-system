using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace FoodDelivery;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(FoodDeliveryApplicationContractsModule),
    typeof(FoodDeliveryDomainModule))]
public class FoodDeliveryApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<FoodDeliveryApplicationModule>();
        });
    }
}
