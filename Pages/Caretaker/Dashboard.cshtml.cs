using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Caretaker
{
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DashboardModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public required CaretakerDashboardViewModel DashboardData { get; set; }

        public async Task OnGetAsync()
        {
            DashboardData = new CaretakerDashboardViewModel
            {
                TotalIssues = await _dbContext.Issues.CountAsync(),
                ResolvedIssues = await _dbContext.Issues.CountAsync(i => i.IsResolved),
                FeedbackCount = await _dbContext.Feedbacks.CountAsync()
            };
        }
    }
}