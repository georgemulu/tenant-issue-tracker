using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Tenant
{
    public class SubmitIssueModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public SubmitIssueModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Issue Issue { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set the reported date
            Issue.ReportedDate = DateTime.Now;

            // Add the issue to the database
            _dbContext.Issues.Add(Issue);
            await _dbContext.SaveChangesAsync();

            // Set a success message
            TempData["Success"] = "Issue submitted successfully!";

            // Redirect to the same page
            return RedirectToPage();
        }
    }
}