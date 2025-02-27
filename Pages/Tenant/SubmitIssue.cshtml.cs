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
            _logger.LogInformation("OnPostAsync method called.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid.");
                return Page();
            }

            // Set the current user as the reporter of the issue
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("User not found.");
                return NotFound("User not found.");
            }
            _logger.LogInformation("User found: {UserId}", user.Id);

            // Link the issue to the current user
            Issue.ApplicationUserId = user.Id;
            Issue.ReportedDate = DateTime.UtcNow;
            _logger.LogInformation("Issue assigned to user {UserId}: {IssueTitle}", user.Id, Issue.Title);

            // Save the issue to the database
            _dbContext.Issues.Add(Issue);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Issue saved to the database: {IssueId}", Issue.Id);

            return RedirectToPage("/Tenant/ViewIssues"); // Redirect to the "View My Issues" page
        }
    }
}