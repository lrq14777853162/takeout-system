namespace FoodDelivery.Permissions;

public static class FoodDeliveryPermissions
{
    public const string GroupName = "FoodDelivery";

    public static class Orders
    {
        public const string Default = GroupName + ".Orders";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Accept = Default + ".Accept";
        public const string Cancel = Default + ".Cancel";
    }

    public static class Merchants
    {
        public const string Default = GroupName + ".Merchants";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Audit = Default + ".Audit";
    }

    public static class Products
    {
        public const string Default = GroupName + ".Products";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Riders
    {
        public const string Default = GroupName + ".Riders";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Audit = Default + ".Audit";
    }

    public static class Admin
    {
        public const string Default = GroupName + ".Admin";
        public const string Dashboard = Default + ".Dashboard";
        public const string UserManage = Default + ".UserManage";
        public const string Finance = Default + ".Finance";
    }

    public static class Coupons
    {
        public const string Default = GroupName + ".Coupons";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
