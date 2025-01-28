using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TenantIssueTracker.Models;
using Microsoft.AspNetCore.Identity;
using TenantIssueTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace TenantIssueTracker.Pages.Tenant
{
    public class ViewIssuesModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ViewIssuesModel(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IList<Issue> Issues { get; set; } = new List<Issue>();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return;
            }

        // Fetch issues submitted by the current user
            Issues = await _dbContext.Issues
                .Where(i => i.ApplicationUserId == user.Id)
                .OrderByDescending(i => i.ReportedDate)
                .ToListAsync();
        }
    }
}