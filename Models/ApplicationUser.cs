using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        public string FirstName { get; set; } = string.Empty; // Initialize

        [Required]
        [PersonalData]
        public string LastName { get; set; } = string.Empty; // Initialize

        [Required]
        [PersonalData]
        public string ApartmentNumber { get; set; } = string.Empty; // Initialize

        public string Name { get; set; } = string.Empty; // Initialize

        public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>(); // Initialize
    }
}