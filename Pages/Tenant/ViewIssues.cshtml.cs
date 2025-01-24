using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Tenant
{
    public class ViewIssuesModel : PageModel
    {
        public List<Issue> Issues { get; set; } = new List<Issue>();

        public void OnGet()
        {
            // Fetch issues for the current user (example data)
            Issues = new List<Issue>
            {
                new Issue
                {
                    Description = "Leaky faucet",
                    Category = "Plumbing",
                    ReportedDate = DateTime.Now.AddDays(-2),
                    IsResolved = false
                },
                new Issue
                {
                    Description = "Broken window",
                    Category = "Maintenance",
                    ReportedDate = DateTime.Now.AddDays(-5),
                    IsResolved = true
                }
            };
        }
    }
}