using Microsoft.AspNetCore.Identity;

namespace TenantIssueTracker.Models;

public class ApplicationUser : IdentityUser
{
    public string ApartmentNumber { get; set; } = string.Empty;
    public bool IsCaretaker { get; set; }
    public ICollection<Issue> Issues { get; set; } = new List<Issue>();
}