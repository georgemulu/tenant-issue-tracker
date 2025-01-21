using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TenantIssueTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [PersonalData]
        public string ApartmentNumber { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;


        public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}