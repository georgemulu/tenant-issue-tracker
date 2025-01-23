using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;
using Microsoft.AspNetCore.Identity;

public class FeedbackController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public FeedbackController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize(Roles = "Tenant")]
    public async Task<IActionResult> Create(int issueId)
    {
        var issue = await _context.Issues
            .FirstOrDefaultAsync(i => i.Id == issueId);

        if (issue == null)
        {
            return NotFound();
        }

        // Check if the issue is resolved
        if (!issue.IsResolved)
        {
            TempData["Error"] = "Feedback can only be provided for resolved issues.";
            return RedirectToAction("Index", "Issues");
        }

        var feedback = new Feedback
        {
            IssueId = issueId,
            TenantName = issue.TenantName, // Pre-fill tenant name from the issue
            ApartmentNumber = issue.ApartmentNumber // Pre-fill apartment number from the issue
        };

        return View(feedback);
    }

    [HttpPost]
    [Authorize(Roles = "Tenant")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IssueId,TenantName,ApartmentNumber,Content")] Feedback feedback)
    {
        if (ModelState.IsValid)
        {
            // Set additional properties
            feedback.SubmittedOn = DateTime.Now;
            feedback.CreatedDate = DateTime.Now;

            _context.Add(feedback);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Feedback submitted successfully!";
            return RedirectToAction("Index", "Issues");
        }

        return View(feedback);
    }

    [Authorize(Roles = "Caretaker")]
    public async Task<IActionResult> Index()
    {
        var feedbacks = await _context.Feedbacks
            .Include(f => f.Issue) // Include the related issue
            .OrderByDescending(f => f.CreatedDate) // Order by creation date
            .ToListAsync();

        return View(feedbacks);
    }
}