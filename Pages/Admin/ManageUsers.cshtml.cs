using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ManageUsersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;


        public ManageUsersModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // List of users to display
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName ?? "N/A",
                    LastName = user.LastName ?? "N/A",
                    Email = user.Email ?? "N/A",
                    Role = roles.FirstOrDefault() ?? "Tenant"
                });
            }

            Users = userViewModels;
            return Page();
        }

        public async Task<IActionResult> OnPostChangeRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return Page();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove all existing roles
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove old roles.");
                return Page();
            }

            // Assign the new role
            var addResult = await _userManager.AddToRoleAsync(user, newRole);
            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add new role.");
                return Page();
            }

            // Explicitly save changes
            await _userManager.UpdateAsync(user);

            // Refresh user to ensure updates persist
            user = await _userManager.FindByIdAsync(userId);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return Page();
            }

            // Remove user's associated issues before deleting the user
            var issues = _context.Issues.Where(i => i.ApplicationUserId == userId);
            _context.Issues.RemoveRange(issues);
            await _context.SaveChangesAsync(); // Commit issue deletions

            // Now delete the user
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to delete user.");
                return Page();
            }

            return RedirectToPage();
        }

        public class UserViewModel
        {
            public string Id { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Role { get; set; } = "Tenant"; // Default role if not assigned
        }
    }
}
