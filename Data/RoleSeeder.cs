using Microsoft.AspNetCore.Identity;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(ApplicationRoles.Tenant))
            {
                await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Tenant));
            }
            
            if (!await roleManager.RoleExistsAsync(ApplicationRoles.Caretaker))
            {
                await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Caretaker));
            }

            if (!await roleManager.RoleExistsAsync(ApplicationRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin));
            }
        }
    }
}