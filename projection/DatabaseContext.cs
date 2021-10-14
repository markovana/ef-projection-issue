using Microsoft.EntityFrameworkCore;
using System;

namespace projection
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Claim> ClaimCases { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<TermsAndConditions> TermsAndConditions { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Initial Catalog=ef-projection;Integrated Security=false;User ID=sa;Password=Password1234;");
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>().HasKey(x => x.Id);
            modelBuilder.Entity<Claim>().HasIndex(p => p.ClaimNumber).IsUnique();
            modelBuilder.Entity<Claim>().HasOne(x => x.Policy)
                .WithMany(x => x.ClaimCases)
                .HasForeignKey(x => x.PolicyId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Claim>().Property(p => p.ClaimNumber)
                .HasMaxLength(50)
                .IsRequired();
            
            
            modelBuilder.Entity<Policy>().HasKey(p => p.Id);
            modelBuilder.Entity<Policy>().Property(p => p.PolicyNumber)
                .HasMaxLength(50)
                .IsRequired();
            
            
            modelBuilder.Entity<TermsAndConditions>().HasKey(p => p.Id);
            modelBuilder.Entity<TermsAndConditions>().Property(x => x.Code)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<TermsAndConditions>().Property(x => x.Label)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<TermsAndConditions>().HasOne(x => x.Policy)
                .WithMany(policy => policy.TermsAndConditions)
                .HasForeignKey(x => x.PolicyId);
            

            modelBuilder.Entity<Claim>()
                .HasData(new Claim { Id = 1, ClaimNumber = "ClaimNumber"});

            base.OnModelCreating(modelBuilder);
        }
    }
}
