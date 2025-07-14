using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models;

public class Transaction : BaseModel<long>
{
    public long InvestorId { get; private init; }
    public long FromWalletId { get; private init; }
    public long ToWalletId { get; private init; }

    public decimal Amount { get; private init; }
    public string TransactionReference { get; private init; }
    public string Description { get; private init; }

    public string CorrelationId { get; private init; }

    private Transaction()
    {
    }

    private Transaction(long investorId, long fromWalletId, long toWalletId, decimal amount, string description,
        string transactionReference, string correlationId)
    {
        InvestorId = investorId;
        FromWalletId = fromWalletId;
        ToWalletId = toWalletId;
        Amount = amount;
        TransactionReference = transactionReference;
        Description = description;
        CreationDate = DateTime.UtcNow;
        CorrelationId = correlationId;
    }

    public static Transaction Create(long investorId, Wallet fromWallet, Wallet toWallet, decimal amount,
        string description, string correlationId)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<long>(investorId, nameof(investorId));
        Guard.AssertArgumentNotNull(fromWallet, nameof(fromWallet));
        Guard.AssertArgumentNotNull(toWallet, nameof(toWallet));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));
        Guard.AssertArgumentNotNullOrEmptyOrWhitespace(description, nameof(description));
        Guard.AssertArgumentNotNullOrEmptyOrWhitespace(correlationId, nameof(correlationId));

        // Guard.AssertArgumentNotEquals(fromWalletId, toWalletId, "From and To wallet IDs must be different.");

        var trx = new Transaction(investorId, fromWallet.Id, toWallet.Id, amount, description,
            GenerateTransactionReference(investorId, fromWallet.Id, toWallet.Id), correlationId);

        return trx;
    }

    private static string GenerateTransactionReference(long investorId, long fromWalletId, long toWalletId)
    {
        return $"{investorId}-{fromWalletId}-{toWalletId}-{DateTime.UtcNow:yyyyMMddHHmmss}";
    }
}