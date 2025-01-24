using System;
using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public string TenantName { get; set; } = string.Empty;

        [Required]
        public string ApartmentNumber { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Relationship to Issue
        public int IssueId { get; set; }
        public Issue Issue { get; set; } = null!;
    }
}