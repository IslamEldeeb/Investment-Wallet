using DinarInvestments.Domain.Models;
using DinarInvestments.Domain.Repositories;

namespace DinarInvestments.Domain.DomainServices;

public interface IInvestmentOpportunityDomainService
{
    Task EnsureInvestmentAmountMeetsMinimumAsync(
        int investmentOpportunityId, decimal amount);
}

public class InvestmentOpportunityDomainService(
    IGenericRepository<InvestmentOpportunity, int> investmentOpportunityRepository)
    : IInvestmentOpportunityDomainService
{
    public async Task EnsureInvestmentAmountMeetsMinimumAsync(int investmentOpportunityId, decimal amount)
    {
        var opportunity = await investmentOpportunityRepository.GetByIdAsync(investmentOpportunityId);
        if (opportunity == null)
        {
            throw new ArgumentException($"Investment opportunity with ID {investmentOpportunityId} does not exist.");
        }

        if (amount < opportunity.MinimumInvestmentAmount)
        {
            throw new ArgumentException(
                $"Investment amount {amount} is less than the minimum required amount of {opportunity.MinimumInvestmentAmount}.");
        }
    }
}