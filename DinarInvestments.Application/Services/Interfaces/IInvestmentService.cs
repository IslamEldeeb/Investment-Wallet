using DinarInvestments.Application.Dtos.Investments;

namespace DinarInvestments.Application.Services.Interfaces;

public interface IInvestmentService
{
    Task AddInvestment(InvestToOpportunityDto input);
}