using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TenantIssueTracker.Models;
using Microsoft.AspNetCore.Identity;
using TenantIssueTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace TenantIssueTracker.Pages.Tenant
{
    public class ViewIssuesModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ViewIssuesModel> _logger;

        public ViewIssuesModel(
            ApplicationDbContext dbContext, 
            UserManager<ApplicationUser> userManager,
            ILogger<ViewIssuesModel> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _logger = logger;
        }

        public IList<Issue> Issues { get; set; } = new List<Issue>();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("User not found.");
                return;
            }
            _logger.LogInformation("User found: {UserId}", user.Id);

            // Fetch issues submitted by the current user
            Issues = await _dbContext.Issues
                .Where(i => i.ApplicationUserId == user.Id)
                .OrderByDescending(i => i.ReportedDate)
                .ToListAsync();
            _logger.LogInformation("Fetched {IssueCount} issues for user {UserId}.", Issues.Count, user.Id);
        }
    }
}