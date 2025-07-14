using DinarInvestments.Domain.DomainServices;
using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models;

public class Investor : ModelBase
{
    public string Name { get; private set; }

    public string Email { get; private set; }

    public List<Wallet> Wallets { get; private set; }

    public List<Investment> Investments { get; private set; }

    public List<Transaction> Transactions { get; private set; }

    private Investor()
    {
    }

    private Investor(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public static Investor Create(string name, string email)
    {
        Guard.AssertArgumentNotNullOrEmptyOrWhitespace(name, nameof(name));
        Guard.AssertArgumentNotNullOrEmptyOrWhitespace(email, nameof(email));

        return new Investor(name, email);
    }

    #region Wallet Operations

    public Wallet GetOrAddWallet(WalletType walletType)
    {
        Guard.AssertEnumValue(walletType, nameof(walletType));

        Wallets ??= new List<Wallet>();

        var wallet = Wallets.FirstOrDefault(w => w.Type == walletType);
        if (wallet != null)
        {
            return wallet;
        }

        var newWallet = new Wallet(Id, walletType);
        Wallets.Add(newWallet);
        return newWallet;
    }

    public string FundMainWallet(decimal amount)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));

        var fromWallet = GetOrAddWallet(WalletType.Funding);
        var toWallet = GetOrAddWallet(WalletType.Main);
        return AddTransaction(fromWallet, toWallet, amount, "Funding main wallet");
    }

    private string FundInvestmentWallet(decimal amount, string description)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));

        var fromWallet = GetOrAddWallet(WalletType.Main);
        var toWallet = GetOrAddWallet(WalletType.Investment);
        return AddTransaction(fromWallet, toWallet, amount, description);
    }

    #endregion


    #region Transactions

    private string AddTransaction(Wallet fromWallet, Wallet toWallet, decimal amount, string description)
    {
        Guard.AssertArgumentNotNull(fromWallet, nameof(fromWallet));
        Guard.AssertArgumentNotNull(toWallet, nameof(toWallet));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));
        Guard.AssertArgumentNotNullOrEmptyOrWhitespace(description, nameof(description));

        if (fromWallet.InvestorId != Id || toWallet.InvestorId != Id)
        {
            throw new InvalidOperationException("Transaction wallets do not belong to this investor.");
        }

        if (fromWallet.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient balance in the from wallet.");
        }

        fromWallet.Deduct(amount);
        toWallet.Fund(amount);

        var transaction = Transaction.Create(Id, fromWallet.Id, toWallet.Id, amount, description);
        AddTransaction(transaction);

        return transaction.TransactionReference;
    }

    private void AddTransaction(Transaction transaction)
    {
        Guard.AssertArgumentNotNull(transaction, nameof(transaction));
        Guard.AssertArgumentEquals(transaction.InvestorId, Id, "Transaction does not belong to this investor.");

        Transactions ??= new List<Transaction>();
        Transactions.Add(transaction);
    }

    #endregion


    #region Investment Operations

    public async Task AddInvestment(long investmentOpportunityId, decimal amount,
        IInvestmentOpportunityDomainService investmentOpportunityDomainService)

    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<long>(investmentOpportunityId, nameof(investmentOpportunityId));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));
        Guard.AssertArgumentNotNull(investmentOpportunityDomainService, nameof(investmentOpportunityDomainService));

        if (!CanInvest(amount))
        {
            throw new InvalidOperationException("Insufficient balance in main wallet to invest.");
        }

        if (!await investmentOpportunityDomainService.EnsureInvestmentAmountMeetsMinimumAsync(investmentOpportunityId,
                amount))
        {
            throw new InvalidOperationException(
                "Investment amount does not meet the minimum requirement for this opportunity.");
        }

        var investment = Investment.Create(Id, investmentOpportunityId, amount);

        var trxRef = FundInvestmentWallet(amount, "Funding investment wallet for investment opportunity");

        investment.Approve(trxRef);

        AddInvestment(investment);
    }

    private void AddInvestment(Investment investment)
    {
        Guard.AssertArgumentNotNull(investment, nameof(investment));
        Guard.AssertArgumentEquals(investment.InvestorId, Id, "Investment does not belong to this investor.");

        Investments ??= [];
        Investments.Add(investment);
    }

    private bool CanInvest(decimal amount)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));

        var mainWallet = GetOrAddWallet(WalletType.Main);
        return mainWallet.Balance >= amount;
    }

    #endregion
}