using DinarInvestments.Domain.Models;
using DinarInvestments.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DinarInvestments.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<InvestmentOpportunity> InvestmentOpportunities { get; set; }

        public DbSet<Investor> Investors { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InvestorConfiguration());
            modelBuilder.ApplyConfiguration(new InvestmentOpportunityConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvestmentOpportunity>().HasData(
                new InvestmentOpportunity(
                    "Real Estate Fund",
                    1000
                ),
                new InvestmentOpportunity(
                    "Tech Growth Fund",
                    500
                ),
                new InvestmentOpportunity(
                    "SME Sukuk",
                    250
                )
            );
        }
    }
}