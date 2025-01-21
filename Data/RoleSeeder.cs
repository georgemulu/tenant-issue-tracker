using Microsoft.AspNetCore.Identity;

namespace TenantIssueTracker.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Define your roles
            string[] roleNames = { "Admin", "Tenant", "Caretaker" };

            foreach (var roleName in roleNames)
            {
                // Check if role already exists
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    // Create role if it doesn't exist
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}