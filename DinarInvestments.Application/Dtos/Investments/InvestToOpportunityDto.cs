namespace DinarInvestments.Application.Dtos.Investments;

public class InvestToOpportunityDto
{
    public long InvestorId { get; set; }
    public int InvestmentOpportunityId { get; set; }
    public decimal Amount { get; set; }
}