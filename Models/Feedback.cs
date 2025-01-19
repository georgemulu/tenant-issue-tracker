using System;
using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public int IssueId { get; set; }

        // Navigation Property
        public virtual Issue Issue { get; set; } = null!;
    }
}
