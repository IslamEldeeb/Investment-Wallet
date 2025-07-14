namespace DinarInvestments.Domain.DomainServices;

public interface IInvestmentOpportunityDomainService
{
    Task<bool> EnsureInvestmentAmountMeetsMinimumAsync(
        long investmentOpportunityId, decimal amount);
}



public class InvestmentOpportunityDomainService :IInvestmentOpportunityDomainService
{
    public Task<bool> EnsureInvestmentAmountMeetsMinimumAsync(long investmentOpportunityId, decimal amount)
    {
        throw new NotImplementedException();
    }
}