using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Data;
using TenantIssueTracker.Models;
using System.Security.Claims;

namespace TenantIssueTracker.Controllers
{
    [Authorize(Roles = ApplicationRoles.Caretaker)]
    public class IssueManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var issues = await _context.Issues
                .Include(i => i.Tenant)
                .OrderByDescending(i => i.CreatedDate)
                .ToListAsync();

            return View(issues);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.Tenant)
                .Include(i => i.Feedback)
                .Include(i => i.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, IssueStatus status)
        {
            var issue = await _context.Issues.FindAsync(id);
            
            if (issue == null)
            {
                return NotFound();
            }

            issue.Status = status;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = issue.Id });
        }
        // Add these methods to your existing IssueManagementController
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> AddComment(CommentViewModel model)
{
    if (ModelState.IsValid)
    {
        var comment = new Comment
        {
            Content = model.Content,
            IssueId = model.IssueId,
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Details), new { id = model.IssueId });
}
    }
}