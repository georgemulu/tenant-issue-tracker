using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Tenant
{
    [Authorize(Roles = "Tenant")]
    public class SubmitFeedbackModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public SubmitFeedbackModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Feedback = new Feedback(); // Initialize to avoid null warning
        }

        [BindProperty]
        public Feedback Feedback { get; set; }

        public async Task<IActionResult> OnGetAsync(int issueId)
        {
            var issue = await _dbContext.Issues.FirstOrDefaultAsync(i => i.Id == issueId && i.IsResolved);
            if (issue == null)
            {
                return NotFound("Issue not found or not resolved.");
            }

            if (User.Identity?.Name == null)
            {
                return RedirectToPage("/Account/Login");
            }

            Feedback = new Feedback
            {
                IssueId = issueId,
                TenantId = User.Identity.Name,
                Comment = string.Empty,
                Rating = 1,
                Issue = issue
                // Removed Tenant initialization as it will be handled by EF Core
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbContext.Feedbacks.Add(Feedback);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/Tenant/ViewIssues");
        }
    }
}