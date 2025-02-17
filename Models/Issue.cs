using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models
{
    public class Issue
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty; // Initialize to empty string

        [Required]
        public string Description { get; set; } = string.Empty; // Initialize to empty string

        [Required]
        public string Category { get; set; } = string.Empty; // Initialize to empty string

        [Required]
        public DateTime ReportedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsResolved { get; set; } = false;

        // New fields for resolution details
        public string? ResolutionComment { get; set; } = string.Empty; // Comment when resolving the issue
        public DateTime? ResolvedDate { get; set; } // Date when the issue was resolved

        // Foreign key to ApplicationUser
        public string ApplicationUserId { get; set; } = string.Empty; // Initialize to empty string

        // Navigation properties
        public ApplicationUser ApplicationUser { get; set; } = new ApplicationUser(); // Initialize
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>(); // Initialize
    }
}