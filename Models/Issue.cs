using System;
using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models
{
    public class Issue
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        [Required]
        public IssueStatus Status { get; set; } = IssueStatus.Pending;

        // Changed to nullable
        public IssuePriority? Priority { get; set; } = IssuePriority.Medium;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        // Navigation Properties
        public virtual ApplicationUser Tenant { get; set; } = null!;
        public virtual Feedback? Feedback { get; set; }
        // Add this to your Issue.cs model
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}