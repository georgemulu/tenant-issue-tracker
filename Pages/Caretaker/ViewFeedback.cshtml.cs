using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Caretaker
{
    public class ViewFeedbackModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ViewFeedbackModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Feedback> Feedbacks { get; set; } = new();

        public async Task OnGetAsync()
        {
            Feedbacks = await _dbContext.Feedbacks.ToListAsync();
        }
    }
}