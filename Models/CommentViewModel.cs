using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models
{
    public class CommentViewModel
    {
        public int IssueId { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; } = string.Empty;
    }
}