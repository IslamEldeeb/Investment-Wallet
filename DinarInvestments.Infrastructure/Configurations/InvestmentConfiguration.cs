using DinarInvestments.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DinarInvestments.Infrastructure.Configurations;

public class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
{
    public void Configure(EntityTypeBuilder<Investment> builder)
    {
        builder.Property(i => i.Id).UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(i => i.InvestorId)
            .IsRequired();
             
        builder.Property(i => i.InvestmentOpportunityId)
            .IsRequired();
        builder.HasOne<InvestmentOpportunity>()
            .WithMany()
            .HasForeignKey(i => i.InvestmentOpportunityId);

        builder.Property(i => i.Amount).IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.CreationDate).IsRequired()
            .HasColumnType("datetime");
    }
}