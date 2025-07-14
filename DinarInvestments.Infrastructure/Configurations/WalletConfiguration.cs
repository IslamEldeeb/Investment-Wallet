using DinarInvestments.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DinarInvestments.Infrastructure.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.Property(w => w.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(w => w.InvestorId)
            .IsRequired();

        builder.Property(w => w.Type)
            .IsRequired();

        builder.Property(w => w.Balance)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasIndex(w => new { w.InvestorId, w.Type })
            .IsUnique()
            .HasDatabaseName("IX_Wallets_InvestorId_Type");

        // Indexes 
        builder.HasIndex(w => w.InvestorId)
            .HasDatabaseName("IX_Wallets_InvestorId");

        builder.HasIndex(w => new { w.InvestorId, w.Type })
            .IsUnique()
            .HasDatabaseName("IX_Wallets_InvestorId_Type");
    }
}