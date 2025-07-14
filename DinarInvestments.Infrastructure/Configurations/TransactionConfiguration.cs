using DinarInvestments.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DinarInvestments.Infrastructure.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(i => i.Id).UseIdentityColumn();

        builder.Property(t => t.InvestorId)
            .IsRequired();

        builder.Property(t => t.FromWalletId)
            .IsRequired();
        builder.HasOne<Wallet>()
            .WithMany()
            .HasForeignKey(t => t.FromWalletId);

        builder.Property(t => t.ToWalletId)
            .IsRequired();
        builder.HasOne<Wallet>()
            .WithMany()
            .HasForeignKey(t => t.ToWalletId);

        builder.Property(t => t.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.CreationDate)
            .IsRequired();

        builder.Property(t => t.TransactionReference)
            .IsRequired()
            .HasMaxLength(50);

        // Unique constraint for TransactionReference
        builder.HasIndex(t => t.TransactionReference)
            .IsUnique()
            .HasDatabaseName("IX_Transaction_TransactionReference");
    }
}