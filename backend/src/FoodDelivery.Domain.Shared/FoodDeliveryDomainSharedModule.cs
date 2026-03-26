using Volo.Abp.Modularity;

namespace FoodDelivery;

[DependsOn(typeof(AbpCoreModule))]
public class FoodDeliveryDomainSharedModule : AbpModule
{
}
