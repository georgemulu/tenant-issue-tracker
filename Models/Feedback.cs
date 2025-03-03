using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantIssueTracker.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Tenant")]
        public string TenantId { get; set; } = string.Empty; // Ensure it's not null by default

        [Required]
        [ForeignKey("Issue")]
        public int IssueId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Feedback cannot exceed 500 characters.")]
        public string Comment { get; set; } = string.Empty; // Same here

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ApplicationUser? Tenant { get; set; } // Virtual for lazy loading
        public virtual Issue? Issue { get; set; } // Same here
    }
}
