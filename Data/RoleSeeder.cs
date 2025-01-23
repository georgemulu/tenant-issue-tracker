using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TenantIssueTracker.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Caretaker", "Tenant" };

            foreach (var role in roles)
            {
                // Check if the role already exists
                if (!await roleManager.RoleExistsAsync(role))
                {
                    // Create the role if it doesn't exist
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}