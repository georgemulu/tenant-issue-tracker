namespace TenantIssueTracker.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public string ApartmentNumber { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime SubmittedOn { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}