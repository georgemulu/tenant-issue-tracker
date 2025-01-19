using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Issue> Issues { get; set; } = null!;
        public DbSet<Feedback> Feedback { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships for Issue
            modelBuilder.Entity<Issue>()
                .HasOne(i => i.Tenant)
                .WithMany(u => u.Issues)
                .HasForeignKey(i => i.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define one-to-one relationship between Issue and Feedback
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Issue)
                .WithOne(i => i.Feedback)
                .HasForeignKey<Feedback>(f => f.IssueId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Issue entity
            // Configure Issue entity
            modelBuilder.Entity<Issue>(entity =>
            {
                entity.Property(i => i.CreatedDate)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(i => i.Status)
                    .HasDefaultValue(IssueStatus.Pending)
                    .HasConversion<string>();

                // Updated Priority configuration
                entity.Property(i => i.Priority)
                    .HasConversion<string>()
                    .IsRequired(false);  // Make it nullable in the database

                entity.Property(i => i.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(i => i.Description)
                    .IsRequired()
                    .HasMaxLength(500);
                });
        }
    }
}