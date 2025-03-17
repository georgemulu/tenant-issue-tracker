using TenantIssueTracker.Models; 
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TenantIssueTracker.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            string[] roles = { "Admin", "Caretaker", "Tenant" };

            foreach (var role in roles)
            {
                // Check if the role already exists
                if (!await roleManager.RoleExistsAsync(role))
                {
                    // Create the role if it doesn't exist
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Ensure there is exactly ONE admin
            var adminEmail = "admin@apartment.com"; // Change this if needed
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123"); // Change password if needed

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
