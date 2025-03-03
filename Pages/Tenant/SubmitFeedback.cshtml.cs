using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Pages.Tenant
{
    [Authorize(Roles = "Tenant")]
    public class SubmitFeedbackModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public SubmitFeedbackModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Feedback = new Feedback(); // Initialize to avoid null warning
        }

        [BindProperty]
        public Feedback Feedback { get; set; }

        public async Task<IActionResult> OnGetAsync(int issueId)
        {
            var issue = await _dbContext.Issues.FirstOrDefaultAsync(i => i.Id == issueId && i.IsResolved);
            if (issue == null)
            {
                return NotFound("Issue not found or not resolved.");
            }

            if (User.Identity?.Name == null)
            {
                return RedirectToPage("/Account/Login");
            }

            Feedback = new Feedback
            {
                IssueId = issueId,
                TenantId = User.Identity.Name,
                Comment = string.Empty,
                Rating = 1,
                Issue = issue
                // Removed Tenant initialization as it will be handled by EF Core
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("OnPostAsync triggered!");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            if (Feedback == null)
            {
                Console.WriteLine("Feedback is null");
                return Page();
            }

            Console.WriteLine($"Feedback IssueId: {Feedback.IssueId}");
            Console.WriteLine($"Feedback TenantId: {Feedback.TenantId}");
            Console.WriteLine($"Feedback Rating: {Feedback.Rating}");
            Console.WriteLine($"Feedback Comment: {Feedback.Comment}");

            try
            {
                _dbContext.Feedbacks.Add(Feedback);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine("Feedback successfully saved");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving feedback: {ex.Message}");
                TempData["Error"] = "Failed to save feedback.";
                return Page();
            }

            TempData["Success"] = "Feedback submitted successfully!";
            return RedirectToPage("/Tenant/ViewIssues");
        }
    }
}