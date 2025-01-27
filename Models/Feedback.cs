using System;
using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models
{
    public class Feedback
{
    public int Id { get; set; }

    [Required]
    public string TenantId { get; set; } = string.Empty; // Initialize to empty string

    [Required]
    public int IssueId { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "Feedback cannot exceed 500 characters.")]
    public string Comment { get; set; } = string.Empty; // Initialize to empty string

    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; set; }

    public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ApplicationUser Tenant { get; set; } = new ApplicationUser(); // Initialize
    public Issue Issue { get; set; } = new Issue(); // Initialize
}
}