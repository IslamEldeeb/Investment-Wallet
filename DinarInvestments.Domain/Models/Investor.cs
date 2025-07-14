using DinarInvestments.Domain.DomainServices;
using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models;

public class Investor : BaseModel<long>
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

    public void UpdateInfo(string name, string email)
    {
        Guard.AssertArgumentNotNullOrEmptyOrWhitespace(name, nameof(name));
        Guard.AssertArgumentNotNullOrEmptyOrWhitespace(email, nameof(email));

        Name = name;
        Email = email;
    }

    #region Wallet Operations

    public string FundMainWallet(decimal amount, string correlationId)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));

        var fromWallet = GetOrAddWallet(WalletType.Funding);
        var toWallet = GetOrAddWallet(WalletType.Main);
        return AddTransaction(fromWallet, toWallet, amount, "Funding main wallet", correlationId);
    }

    public decimal GetMainWalletBalance()
    {
        var mainWallet = GetOrAddWallet(WalletType.Main);
        return mainWallet.Balance;
    }
    
    
    private Wallet GetOrAddWallet(WalletType walletType)
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


    private string FundInvestmentWallet(decimal amount, string description, string correlationId)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));

        var fromWallet = GetOrAddWallet(WalletType.Main);
        var toWallet = GetOrAddWallet(WalletType.Investment);
        return AddTransaction(fromWallet, toWallet, amount, description, correlationId);
    }

    
    
    #endregion


    #region Transactions

    private string AddTransaction(Wallet fromWallet, Wallet toWallet, decimal amount, string description,
        string correlationId)
    {
        Guard.AssertArgumentNotNull(fromWallet, nameof(fromWallet));
        Guard.AssertArgumentNotNull(toWallet, nameof(toWallet));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));
        Guard.AssertArgumentNotNullOrEmptyOrWhitespace(description, nameof(description));

        if (fromWallet.InvestorId != Id || toWallet.InvestorId != Id)
        {
            throw new InvalidOperationException("Transaction wallets do not belong to this investor.");
        }

        if (fromWallet.Type == WalletType.Main && fromWallet.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient balance in the from wallet.");
        }

        fromWallet.Deduct(amount);
        toWallet.Fund(amount);

        var transaction = Transaction.Create(Id, fromWallet, toWallet, amount, description, correlationId);
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

    public async Task AddInvestment(int investmentOpportunityId, decimal amount,
        IInvestmentOpportunityDomainService investmentOpportunityDomainService)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<int>(investmentOpportunityId, nameof(investmentOpportunityId));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));
        Guard.AssertArgumentNotNull(investmentOpportunityDomainService, nameof(investmentOpportunityDomainService));

        await CanInvest(investmentOpportunityId, amount, investmentOpportunityDomainService);

        var investment = Investment.Create(Id, investmentOpportunityId, amount);

        var trxRef = FundInvestmentWallet(amount, "Funding investment wallet for investment opportunity",
            $"{investmentOpportunityId}-{Id}-{DateTime.UtcNow:yyyyMMddHHmmss}");
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

    private async Task CanInvest(int investmentOpportunityId, decimal amount,
        IInvestmentOpportunityDomainService investmentOpportunityDomainService)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));

        var mainWallet = GetOrAddWallet(WalletType.Main);
        if (mainWallet.Balance < amount)
            throw new InvalidOperationException("Insufficient balance in main wallet to invest.");

        await investmentOpportunityDomainService.EnsureInvestmentAmountMeetsMinimumAsync(investmentOpportunityId,
            amount);
    }

    #endregion
}