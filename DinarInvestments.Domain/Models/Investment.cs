using DinarInvestments.Domain.DomainServices;
using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models;

public class Investment : BaseModel<long>
{
    public long InvestorId { get; private set; }
    public int InvestmentOpportunityId { get; private set; }
    public decimal Amount { get; private set; }
    public InvestmentStatus Status { get; private set; }
    public string TransactionReference { get; set; }

    private Investment()
    {
    }

    private Investment(long investorId, int investmentOpportunityId, decimal amount)
    {
        InvestorId = investorId;
        InvestmentOpportunityId = investmentOpportunityId;
        Amount = amount;
        Status = InvestmentStatus.Pending;
        CreationDate = DateTime.UtcNow;
    }

    public static Investment Create(long investorId, int investmentOpportunityId, decimal amount)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<long>(investorId, nameof(investorId));
        Guard.AssertArgumentNotLessThanOrEqualToZero<int>(investmentOpportunityId,
            nameof(investmentOpportunityId));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(amount, nameof(amount));

        return new Investment(investorId, investmentOpportunityId, amount);
    }
    
    public void Approve(string transactionReference)
    {
        Guard.AssertArgumentNotNullOrEmptyOrWhitespace(transactionReference, nameof(transactionReference));

        if (Status != InvestmentStatus.Pending)
        {
            throw new InvalidOperationException("Investment can only be approved if it is pending.");
        }

        Status = InvestmentStatus.Approved;
        TransactionReference = transactionReference;
    }
    
    // Mocked method to simulate rejection of an investment
    public void Reject()
    {
        if (Status != InvestmentStatus.Pending)
        {
            throw new InvalidOperationException("Investment can only be rejected if it is pending.");
        }

        Status = InvestmentStatus.Rejected;
    }
}

public enum InvestmentStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2
}