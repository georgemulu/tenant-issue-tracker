using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Caretaker
{
    [Authorize(Roles = "Caretaker")]
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DashboardModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CaretakerDashboardViewModel DashboardData { get; set; } = new();

        public List<Issue> ActiveIssues { get; set; } = new(); // List of active (unresolved) issues

        public async Task OnGetAsync()
        {
            // Fetch dashboard statistics
            DashboardData = new CaretakerDashboardViewModel
            {
                TotalIssues = await _dbContext.Issues.CountAsync(),
                ResolvedIssues = await _dbContext.Issues.CountAsync(i => i.IsResolved),
                FeedbackCount = await _dbContext.Feedbacks.CountAsync()
            };

            // Fetch active (unresolved) issues
            ActiveIssues = await _dbContext.Issues
                .Where(i => !i.IsResolved) // Filter unresolved issues
                .Include(i => i.ApplicationUser) // Include tenant details
                .ToListAsync();
        }
    }
}