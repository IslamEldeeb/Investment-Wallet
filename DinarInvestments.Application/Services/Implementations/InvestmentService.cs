using DinarInvestments.Application.Dtos.Investments;
using DinarInvestments.Application.Services.Interfaces;
using DinarInvestments.Domain.DomainServices;
using DinarInvestments.Domain.Repositories;
using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Application.Services.Implementations;

public class InvestmentService(
    IInvestorRepository investorRepository,
    IInvestmentOpportunityDomainService investmentOpportunityDomainService) : IInvestmentService
{
    // Add Investment 

    public async Task AddInvestment(InvestToOpportunityDto input)
    {
        Guard.AssertArgumentNotNull(input, nameof(input));
        Guard.AssertArgumentNotLessThanOrEqualToZero<long>(input.InvestorId, nameof(input.InvestorId));
        Guard.AssertArgumentNotLessThanOrEqualToZero<int>(input.InvestmentOpportunityId,
            nameof(input.InvestmentOpportunityId));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(input.Amount, nameof(input.Amount));

        var investor = await investorRepository.GetInvestorAsync(input.InvestorId);
        if (investor == null)
            throw new InvalidOperationException($"Investor with ID {input.InvestorId} not found.");

        await investor.AddInvestment(input.InvestmentOpportunityId, input.Amount, investmentOpportunityDomainService);

        await investorRepository.SaveChangesAsync();
    }
}