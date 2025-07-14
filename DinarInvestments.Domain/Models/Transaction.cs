using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models;

public class Transaction : BaseModel<long>
{
    public long InvestorId { get; private init; }
    public Guid FromWalletId { get; private init; }
    public Guid ToWalletId { get; private init; }
    public decimal Amount { get; private init; }
    public string TransactionReference { get; private init; }
    public string Description { get; private init; }

    private Transaction()
    {
    }

    private Transaction(long investorId, Guid fromWalletId, Guid toWalletId, decimal amount, string description,
        string transactionReference)
    {
        InvestorId = investorId;
        FromWalletId = fromWalletId;
        ToWalletId = toWalletId;
        Amount = amount;
        TransactionReference = transactionReference;
        Description = description;
        CreationDate = DateTime.UtcNow;
    }

    public static Transaction Create(long investorId, Guid fromWalletId, Guid toWalletId, decimal amount,
        string description)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<long>(investorId, nameof(investorId));
        Guard.AssertArgumentNotNull(fromWalletId, nameof(fromWalletId));
        Guard.AssertArgumentNotNull(toWalletId, nameof(toWalletId));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));
        Guard.AssertArgumentNotNullOrEmptyOrWhitespace(description, nameof(description));

        Guard.AssertArgumentNotEquals(fromWalletId, toWalletId, "From and To wallet IDs must be different.");

        var trx = new Transaction(investorId, fromWalletId, toWalletId, amount, description,
            GenerateTransactionReference(investorId, fromWalletId, toWalletId));

        return trx;
    }

    private static string GenerateTransactionReference(long investorId, Guid fromWalletId, Guid toWalletId)
    {
        return $"{investorId}-{fromWalletId}-{toWalletId}-{DateTime.UtcNow:yyyyMMddHHmmss}";
    }
}