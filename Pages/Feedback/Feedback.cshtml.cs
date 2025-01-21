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

        if (issue.Status != IssueStatus.Resolved)
        {
            TempData["Error"] = "Feedback can only be provided for resolved issues.";
            return RedirectToAction("Index", "Issues");
        }

        var feedback = new Feedback
        {
            IssueId = issueId
        };

        return View(feedback);
    }

    [HttpPost]
    [Authorize(Roles = "Tenant")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IssueId,Comment,Rating")] Feedback feedback)
    {
        if (ModelState.IsValid)
        {
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
    var feedbacks = await _context.Feedback
        .Include(f => f.Issue)
            .ThenInclude(i => i.Tenant)
        .OrderByDescending(f => f.CreatedDate)
        .ToListAsync();

    return View(feedbacks);
    }
}