using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace FoodDelivery;

[DependsOn(
    typeof(FoodDeliveryApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpIdentityHttpApiModule))]
public class FoodDeliveryHttpApiModule : AbpModule
{
}
