using DinarInvestments.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DinarInvestments.Infrastructure.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(i => i.Id).UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(t => t.InvestorId)
            .IsRequired();

        builder.Property(t => t.FromWalletId)
            .IsRequired();
        
        builder.HasOne(t => t.FromWallet)
            .WithMany()
            .HasForeignKey(t => t.FromWalletId)
            .OnDelete(DeleteBehavior.NoAction);
        

        builder.Property(t => t.ToWalletId)
            .IsRequired();
        
        builder.HasOne(t => t.ToWallet)
            .WithMany()
            .HasForeignKey(t => t.ToWalletId)
            .OnDelete(DeleteBehavior.NoAction);

        
        
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
            .HasMaxLength(100);

        builder.Property(t => t.CorrelationId)
            .IsRequired()
            .HasMaxLength(200);

        // Unique constraint for TransactionReference
        builder.HasIndex(t => t.TransactionReference)
            .IsUnique()
            .HasDatabaseName("IX_Transaction_TransactionReference");

        builder.HasIndex(t => t.CorrelationId)
            .IsUnique()
            .HasDatabaseName("IX_Transaction_CorrelationId");
    }
}