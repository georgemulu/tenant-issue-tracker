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

            // Configure the relationship between Issue and ApplicationUser
            modelBuilder.Entity<Issue>()
                .HasOne(i => i.ApplicationUser) // Issue has one ApplicationUser
                .WithMany(u => u.Issues) // ApplicationUser can have many Issues
                .HasForeignKey(i => i.ApplicationUserId) // Foreign key in Issue
                .OnDelete(DeleteBehavior.Restrict); // Disable cascading delete

            // Configure the relationship between Feedback and Issue
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Issue) // Feedback has one Issue
                .WithMany(i => i.Feedbacks) // Issue can have many Feedbacks
                .HasForeignKey(f => f.IssueId) // Foreign key in Feedback
                .OnDelete(DeleteBehavior.Cascade); // Allow cascading delete for Issue -> Feedbacks

            // Configure your entities here if needed
            modelBuilder.Entity<Issue>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Category).IsRequired();
                entity.Property(e => e.ReportedDate).IsRequired();
                entity.Property(e => e.IsResolved).IsRequired();
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IssueId).IsRequired();
            });
        }
    }
}