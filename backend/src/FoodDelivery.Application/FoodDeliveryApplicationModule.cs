using Volo.Abp.Application;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace FoodDelivery;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(FoodDeliveryDomainModule),
    typeof(FoodDeliveryApplicationContractsModule))]
public class FoodDeliveryApplicationModule : AbpModule
{
}
