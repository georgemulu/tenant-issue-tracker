using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models;

public class Issue
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime CreatedDate { get; set; }
    public DateTime? ResolvedDate { get; set; }

    public string ApartmentNumber { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public ApplicationUser? Tenant { get; set; }
    public Feedback? Feedback { get; set; }
}