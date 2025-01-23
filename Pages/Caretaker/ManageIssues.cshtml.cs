using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Caretaker
{
    public class ManageIssuesModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ManageIssuesModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Issue> Issues { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Fetch all issues from the database
            Issues = await _dbContext.Issues.ToListAsync();
        }

        public async Task<IActionResult> OnPostResolveIssueAsync(int id)
        {
            // Find the issue by ID
            var issue = await _dbContext.Issues.FindAsync(id);
            if (issue != null)
            {
                // Mark the issue as resolved
                issue.IsResolved = true;
                await _dbContext.SaveChangesAsync();

                // Set a success message
                TempData["Success"] = "Issue resolved successfully!";
            }
            else
            {
                // Set an error message if the issue is not found
                TempData["Error"] = "Issue not found.";
            }

            // Redirect back to the same page
            return RedirectToPage();
        }
    }
}