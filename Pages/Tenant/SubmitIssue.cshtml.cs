using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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

        public class IssueInputModel
        {
            [Required]
            public string Title { get; set; } = string.Empty;
            
            [Required]
            public string Description { get; set; } = string.Empty;
            
            [Required]
            public string Category { get; set; } = string.Empty;
        }

        [BindProperty]
        public IssueInputModel IssueInput { get; set; } = new IssueInputModel();

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync method called.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid.");
                
                // Log each model state error to help diagnose the issue
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogWarning("Validation error: {ErrorMessage}", error.ErrorMessage);
                    }
                }
                
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

            // Create a new Issue object
            var newIssue = new Issue
            {
                Title = IssueInput.Title,
                Description = IssueInput.Description,
                Category = IssueInput.Category,
                ReportedDate = DateTime.UtcNow,
                IsResolved = false,
                ApplicationUserId = user.Id
            };

            // Save the issue to the database
            _dbContext.Issues.Add(newIssue);
            
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Issue saved to the database: {IssueId}", newIssue.Id);

            return RedirectToPage("/Tenant/ViewIssues"); // Redirect to the "View My Issues" page
        }
    }
}