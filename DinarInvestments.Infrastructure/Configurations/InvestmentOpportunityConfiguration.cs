using DinarInvestments.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DinarInvestments.Infrastructure.Configurations
{
    public class InvestmentOpportunityConfiguration : IEntityTypeConfiguration<InvestmentOpportunity>
    {
        public void Configure(EntityTypeBuilder<InvestmentOpportunity> builder)
        {
            builder.Property(i => i.Id).UseIdentityColumn();

            builder.Property(o => o.Name).IsRequired().HasMaxLength(100);
            builder.Property(o => o.MinimumInvestmentAmount).IsRequired();

            builder.HasMany(o => o.Investments)
                .WithOne()
                .HasForeignKey(inv => inv.InvestmentOpportunityId);
        }
    }
}

