using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASVSSECURITYAUDITOR.Core.Entities;

namespace ASVSSECURITYAUDITOR.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ASVSRequirement> ASVSRequirements { get; set; }

        public DbSet<Assessment> Assessments { get; set; }

        public DbSet<AssessmentItem> AssessmentItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ASVSRequirement>()
                .Property(x => x.RequirementCode)
                .IsRequired();

            modelBuilder.Entity<ASVSRequirement>()
                .Property(x => x.Title)
                .IsRequired();
        }
    }
}