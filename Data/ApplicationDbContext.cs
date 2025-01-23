using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Define your DbSet properties here
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        // Optionally, you can override the OnModelCreating method to configure the model
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