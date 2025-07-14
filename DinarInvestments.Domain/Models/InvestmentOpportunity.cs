using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models;

public class InvestmentOpportunity : ModelBase
{
    public string Name { get; private set; }
    public decimal MinimumInvestmentAmount { get; private set; }
        

    public InvestmentOpportunity(long id, string name, decimal minimumInvestmentAmount)
    {
        Id = id;
        Name = name;
        MinimumInvestmentAmount = minimumInvestmentAmount;
        CreationDate = DateTime.UtcNow;
    }
        
    private InvestmentOpportunity() { }
}