using Microsoft.AspNetCore.Identity;

namespace TenantIssueTracker.Models;

public class ApplicationUser : IdentityUser
{
    public string ApartmentNumber { get; set; }
    public bool IsCaretaker { get; set; }
    public ICollection<Issue> Issues { get; set; }
}