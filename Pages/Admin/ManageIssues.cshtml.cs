using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Models;
using TenantIssueTracker.Data;

namespace TenantIssueTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ManageIssuesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ManageIssuesModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public List<ApplicationUser> Tenants { get; set; } = new();
        public List<Issue> Issues { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string SelectedTenantId { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            // Get all tenants (users who are not admins)
            var users = await _userManager.GetUsersInRoleAsync("Tenant");
            Tenants = users.ToList();

            if (string.IsNullOrEmpty(SelectedTenantId))
            {
                Issues = await _context.Issues
                    .Include(i => i.ApplicationUser) 
                    .ToListAsync();
            }
            else
            {
                Issues = await _context.Issues
                    .Where(i => i.ApplicationUserId == SelectedTenantId)
                    .Include(i => i.ApplicationUser)
                    .ToListAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostResolveAsync(int issueId)
        {
            var issue = await _context.Issues.FindAsync(issueId);
            if (issue == null) return NotFound();

            issue.IsResolved = true;
            await _context.SaveChangesAsync();
            return RedirectToPage(new { tenantId = SelectedTenantId });
        }

        public async Task<IActionResult> OnPostUndoResolveAsync(int issueId)
        {
            var issue = await _context.Issues.FindAsync(issueId);
            if (issue == null) return NotFound();

            issue.IsResolved = false;
            await _context.SaveChangesAsync();
            return RedirectToPage(new { tenantId = SelectedTenantId });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int issueId)
        {
            var issue = await _context.Issues.FindAsync(issueId);
            if (issue == null) return NotFound();

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToPage(new { tenantId = SelectedTenantId });
        }
    }
}
