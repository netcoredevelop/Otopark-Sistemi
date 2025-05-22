namespace WebUI.Auth;

public static class RoleAuthHelper
{
    public static string GetRoleName(string entityName, string action)
    {
        return $"{entityName}_{action}";
    }

    public static class Entities
    {
        public const string Vehicle = "Vehicle";
        public const string VehicleBrand = "VehicleBrand";
        public const string VehicleModel = "VehicleModel";
        public const string VehicleType = "VehicleType";
        public const string VehicleColor = "VehicleColor";
        public const string VehicleYear = "VehicleYear";
        public const string KeyLocation = "KeyLocation";
        public const string LinkingReason = "LinkingReason";
        public const string LinkingRegion = "LinkingRegion";
        public const string Branch = "Branch";
        public const string ParkLocation = "ParkLocation";
        public const string VehicleImage = "VehicleImage";
        public const string Document = "Document";
        public const string EnforcementRecord = "EnforcementRecord";
        public const string EnforcementOffice = "EnforcementOffice";
        public const string Transaction = "Transaction";
        public const string TransactionCategory = "TransactionCategory";
        public const string User = "User";
        public const string Role = "Role";
    }

    public static class Actions
    {
        public const string Add = "Add";
        public const string Edit = "Edit";
        public const string Remove = "Remove";
        public const string View = "View";
    }
} 