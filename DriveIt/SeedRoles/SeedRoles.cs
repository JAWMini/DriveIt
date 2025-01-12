namespace DriveIt.SeedRoles
{
    using Microsoft.AspNetCore.Identity;
    public class SeedRoles
    {
        public static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Employee", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
