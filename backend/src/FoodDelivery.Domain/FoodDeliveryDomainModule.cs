using Volo.Abp.Domain;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace FoodDelivery;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(FoodDeliveryDomainSharedModule))]
public class FoodDeliveryDomainModule : AbpModule
{
}
