using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        [BindProperty]
        public string? ResolutionComment { get; set; } // Bound property for resolution comment

        public async Task OnGetAsync()
        {
            // Fetch all issues, including tenant details
            Issues = await _dbContext.Issues
                .Include(i => i.ApplicationUser) // Include tenant details
                .OrderByDescending(i => i.ReportedDate) // Order by reported date (newest first)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostResolveIssueAsync(int id)
        {
            var issue = await _dbContext.Issues.FindAsync(id);
            if (issue == null)
            {
                TempData["Error"] = "Issue not found.";
                return RedirectToPage();
            }

            // Mark the issue as resolved
            issue.IsResolved = true;
            issue.ResolutionComment = ResolutionComment; // Set the resolution comment
            issue.ResolvedDate = DateTime.UtcNow; // Set the resolved date

            _dbContext.Issues.Update(issue);
            await _dbContext.SaveChangesAsync();

            TempData["Success"] = "Issue marked as resolved successfully.";
            return RedirectToPage();
        }
    }
}