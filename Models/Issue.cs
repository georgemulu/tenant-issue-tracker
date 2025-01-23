namespace TenantIssueTracker.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string TenantName { get; set; } = string.Empty; 
        public string ApartmentNumber { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime ReportedDate { get; set; }
        public bool IsResolved { get; set; }
        public string Feedback { get; set; } = string.Empty;
    }
}