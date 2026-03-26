using FoodDelivery.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace FoodDelivery.Permissions;

public class FoodDeliveryPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(FoodDeliveryPermissions.GroupName, L("Permission:FoodDelivery"));

        var orders = group.AddPermission(FoodDeliveryPermissions.Orders.Default, L("Permission:Orders"));
        orders.AddChild(FoodDeliveryPermissions.Orders.Create, L("Permission:Orders.Create"));
        orders.AddChild(FoodDeliveryPermissions.Orders.Edit, L("Permission:Orders.Edit"));
        orders.AddChild(FoodDeliveryPermissions.Orders.Delete, L("Permission:Orders.Delete"));
        orders.AddChild(FoodDeliveryPermissions.Orders.Accept, L("Permission:Orders.Accept"));
        orders.AddChild(FoodDeliveryPermissions.Orders.Cancel, L("Permission:Orders.Cancel"));

        var merchants = group.AddPermission(FoodDeliveryPermissions.Merchants.Default, L("Permission:Merchants"));
        merchants.AddChild(FoodDeliveryPermissions.Merchants.Create, L("Permission:Merchants.Create"));
        merchants.AddChild(FoodDeliveryPermissions.Merchants.Edit, L("Permission:Merchants.Edit"));
        merchants.AddChild(FoodDeliveryPermissions.Merchants.Delete, L("Permission:Merchants.Delete"));
        merchants.AddChild(FoodDeliveryPermissions.Merchants.Audit, L("Permission:Merchants.Audit"));

        var products = group.AddPermission(FoodDeliveryPermissions.Products.Default, L("Permission:Products"));
        products.AddChild(FoodDeliveryPermissions.Products.Create, L("Permission:Products.Create"));
        products.AddChild(FoodDeliveryPermissions.Products.Edit, L("Permission:Products.Edit"));
        products.AddChild(FoodDeliveryPermissions.Products.Delete, L("Permission:Products.Delete"));

        var riders = group.AddPermission(FoodDeliveryPermissions.Riders.Default, L("Permission:Riders"));
        riders.AddChild(FoodDeliveryPermissions.Riders.Create, L("Permission:Riders.Create"));
        riders.AddChild(FoodDeliveryPermissions.Riders.Edit, L("Permission:Riders.Edit"));
        riders.AddChild(FoodDeliveryPermissions.Riders.Audit, L("Permission:Riders.Audit"));

        var admin = group.AddPermission(FoodDeliveryPermissions.Admin.Default, L("Permission:Admin"));
        admin.AddChild(FoodDeliveryPermissions.Admin.Dashboard, L("Permission:Admin.Dashboard"));
        admin.AddChild(FoodDeliveryPermissions.Admin.UserManage, L("Permission:Admin.UserManage"));
        admin.AddChild(FoodDeliveryPermissions.Admin.Finance, L("Permission:Admin.Finance"));

        var coupons = group.AddPermission(FoodDeliveryPermissions.Coupons.Default, L("Permission:Coupons"));
        coupons.AddChild(FoodDeliveryPermissions.Coupons.Create, L("Permission:Coupons.Create"));
        coupons.AddChild(FoodDeliveryPermissions.Coupons.Edit, L("Permission:Coupons.Edit"));
        coupons.AddChild(FoodDeliveryPermissions.Coupons.Delete, L("Permission:Coupons.Delete"));
    }

    private static LocalizableString L(string name) =>
        LocalizableString.Create<FoodDeliveryResource>(name);
}
