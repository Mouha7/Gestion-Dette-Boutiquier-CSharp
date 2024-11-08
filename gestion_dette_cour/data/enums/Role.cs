using System;

namespace Main.Data.Enums
{
    public enum Role
    {
        ADMIN, BOUTIQUIER, CLIENT

}
    public static class RoleExtensions
    {
        public static Role? getRole(String role)
        {
            foreach (Role r in Enum.GetValues(typeof(Role)))
            {
                if (r.ToString().Equals(role, StringComparison.OrdinalIgnoreCase))
                {
                    return r;
                }
            }
            return null;
        }
    }
}