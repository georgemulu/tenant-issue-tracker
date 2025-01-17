using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models;

public class Issue
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Category { get; set; }  // Electricity, Water, Supplies, etc.

    public string Status { get; set; }    // Pending, InProgress, Resolved
    public DateTime CreatedDate { get; set; }
    public DateTime? ResolvedDate { get; set; }

    public string ApartmentNumber { get; set; }
    public string TenantId { get; set; }
    public ApplicationUser Tenant { get; set; }
    public Feedback Feedback { get; set; }
}