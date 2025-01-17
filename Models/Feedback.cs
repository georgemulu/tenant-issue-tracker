using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models;

public class Feedback
{
    public int Id { get; set; }
    public int IssueId { get; set; }
    public Issue? Issue { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime SubmittedDate { get; set; }
}