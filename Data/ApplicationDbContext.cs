using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TenantIssueTracker.Models;

namespace TenantIssueTracker.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Issue> Issues { get; set; }
    public DbSet<Feedback> Feedback { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Tenant)
            .WithMany(u => u.Issues)
            .HasForeignKey(i => i.TenantId);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Issue)
            .WithOne(i => i.Feedback)
            .HasForeignKey<Feedback>(f => f.IssueId);
    }
}