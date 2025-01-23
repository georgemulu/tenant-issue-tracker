using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure your entities here if needed
            modelBuilder.Entity<Issue>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Category).IsRequired();
                entity.Property(e => e.ReportedDate).IsRequired();
                entity.Property(e => e.IsResolved).IsRequired();
                entity.Property(e => e.Feedback).IsRequired(false); // Feedback is optional
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IssueId).IsRequired();
            });
        }
    }
}