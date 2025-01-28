using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Tenant
{
    public class SubmitIssueModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SubmitIssueModel> _logger;

        public SubmitIssueModel(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            ILogger<SubmitIssueModel> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public Issue Issue { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set the current user as the reporter of the issue
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            Issue.ApplicationUserId = user.Id; // Link the issue to the current user
            Issue.ReportedDate = DateTime.UtcNow; // Set the reported date

            // Save the issue to the database
            _dbContext.Issues.Add(Issue);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Issue submitted by user {UserId}: {IssueTitle}", user.Id, Issue.Title);

            return RedirectToPage("/Tenant/ViewIssues"); // Redirect to the "View My Issues" page
        }
    }
}