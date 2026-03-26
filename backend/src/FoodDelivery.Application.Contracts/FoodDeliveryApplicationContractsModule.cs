using Volo.Abp.Application;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace FoodDelivery;

[DependsOn(
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(FoodDeliveryDomainSharedModule))]
public class FoodDeliveryApplicationContractsModule : AbpModule
{
}
