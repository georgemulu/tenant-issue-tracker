using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Caretaker
{
    [Authorize(Roles = "Caretaker")]
    public class ManageIssuesModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ManageIssuesModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Issue> Issues { get; set; } = new(); // List of all issues

        public async Task OnGetAsync()
        {
            // Fetch all issues, including tenant details
            Issues = await _dbContext.Issues
                .Include(i => i.ApplicationUser) // Include tenant details
                .OrderByDescending(i => i.ReportedDate) // Order by reported date (newest first)
                .ToListAsync();
        }

        // Handles marking an issue as resolved
        public async Task<IActionResult> OnPostResolveAsync(int id)
        {
            var issue = await _dbContext.Issues.FindAsync(id);

            if (issue == null)
            {
                TempData["Error"] = "Issue not found.";
                return RedirectToPage();
            }

            if (issue.IsResolved)
            {
                TempData["Error"] = "This issue is already resolved.";
                return RedirectToPage();
            }

            issue.IsResolved = true;

            try
            {
                await _dbContext.SaveChangesAsync();
                TempData["Success"] = "Issue marked as resolved.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to update issue status.";
            }

            return RedirectToPage();
        }

        // Handles undoing the resolved status of an issue
        public async Task<IActionResult> OnPostUndoResolveAsync(int id)
        {
            var issue = await _dbContext.Issues.FindAsync(id);

            if (issue == null)
            {
                TempData["Error"] = "Issue not found.";
                return RedirectToPage();
            }

            if (!issue.IsResolved)
            {
                // Fixing the incorrect message here
                TempData["Error"] = "This issue hasn't been resolved yet.";
                return RedirectToPage();
            }

            issue.IsResolved = false;

            try
            {
                await _dbContext.SaveChangesAsync();
                TempData["Success"] = "Issue status reverted to unresolved.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to update issue status.";
            }

            return RedirectToPage();
        }
    }
}
