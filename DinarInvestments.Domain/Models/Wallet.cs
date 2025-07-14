using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models;

public class Wallet
{
    public Guid Id { get; private init; }
    public long InvestorId { get; private init; }
    public decimal Balance { get; private set; }

    public WalletType Type { get; private init; }

    public Wallet(long investorId, WalletType type, decimal initialBalance = 0)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<long>(investorId, nameof(investorId));
        Guard.AssertEnumValue(type, nameof(type));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(initialBalance, nameof(initialBalance));

        Id = Guid.NewGuid();
        InvestorId = investorId;
        Balance = initialBalance;
        Type = type;
    }

    private Wallet()
    {
    }

    public void Fund(decimal amount)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));

        Balance += amount;
    }

    public void Deduct(decimal amount)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(Balance - amount, "Insufficient balance");

        Balance -= amount;
    }
}

public enum WalletType
{
    Main = 1,
    Investment = 2, 
    Funding = 3, // Simulates funding from external sources
}