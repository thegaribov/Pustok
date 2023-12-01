namespace Pustok.Contracts
{
    public enum Role
    {
        SuperAdmin,
        Admin,
        Moderator,
        SMM
    }

    public static class RoleNames
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Moderator = "Moderator";
        public const string SMM = "SMM";
    }
}
