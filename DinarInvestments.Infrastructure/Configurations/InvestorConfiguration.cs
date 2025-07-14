using DinarInvestments.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DinarInvestments.Infrastructure.Configurations
{
    public class InvestorConfiguration : IEntityTypeConfiguration<Investor>
    {
        public void Configure(EntityTypeBuilder<Investor> builder)
        {
            builder.Property(i => i.Id).UseIdentityColumn();
            builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Email).IsRequired().HasMaxLength(100);

            builder.HasMany(i => i.Wallets)
                .WithOne()
                .HasForeignKey(w => w.InvestorId);

            builder.HasMany(i => i.Investments)
                .WithOne()
                .HasForeignKey(i => i.InvestorId);

            builder.HasMany(i => i.Transactions)
                .WithOne()
                .HasForeignKey(i => i.InvestorId);
        }
    }
}