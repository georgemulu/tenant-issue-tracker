using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public async Task OnGetAsync()
        {
            // Fetch dashboard statistics
            DashboardData = new CaretakerDashboardViewModel
            {
                TotalIssues = await _dbContext.Issues.CountAsync(),
                ResolvedIssues = await _dbContext.Issues.CountAsync(i => i.IsResolved),
                FeedbackCount = await _dbContext.Feedbacks.CountAsync()
            };

            // Remove the logic for fetching active issues
        }
    }
}