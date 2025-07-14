using DinarInvestments.Domain.Models;
using DinarInvestments.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DinarInvestments.Infrastructure;

public class InvestorDbContext(DbContextOptions<InvestorDbContext> options) : DbContext(options)
{
    public DbSet<InvestmentOpportunity> InvestmentOpportunities { get; set; }

    public DbSet<Investment> Investments { get; set; }

    public DbSet<Investor> Investors { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InvestorConfiguration());
        modelBuilder.ApplyConfiguration(new InvestmentOpportunityConfiguration());
        modelBuilder.ApplyConfiguration(new InvestmentConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new WalletConfiguration());

        SeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvestmentOpportunity>().HasData(
            new InvestmentOpportunity(
                1,
                "Real Estate Fund",
                1000
            ),
            new InvestmentOpportunity(
                2,
                "Tech Growth Fund",
                500
            ),
            new InvestmentOpportunity(
                3,
                "SME Sukuk",
                250
            )
        );
    }
}