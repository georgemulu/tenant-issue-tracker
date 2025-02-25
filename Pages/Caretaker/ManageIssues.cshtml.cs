using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Caretaker
{
    [Authorize(Roles = "Caretaker")]
    public class ManageIssuesModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ManageIssuesModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Issue> Issues { get; set; } = new(); // List of all issues

        public async Task OnGetAsync()
        {
            // Fetch all issues, including tenant details
            Issues = await _dbContext.Issues
                .Include(i => i.ApplicationUser) // Include tenant details
                .OrderByDescending(i => i.ReportedDate) // Order by reported date (newest first)
                .ToListAsync();
        }
    }
}