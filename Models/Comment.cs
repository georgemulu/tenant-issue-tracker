using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; } = null!;

        public int IssueId { get; set; }
        public virtual Issue Issue { get; set; } = null!;
    }
}