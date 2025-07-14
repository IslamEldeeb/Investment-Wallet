using DinarInvestments.Domain.Models.ValueObjects;
using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models
{
    public class InvestmentOpportunity : AggregateRoot
    {
        public string Name { get; private set; }
        public decimal MinimumInvestmentAmount { get; private set; }

        public List<Investment> Investments { get; private set; }


        public InvestmentOpportunity(string name, decimal minimumInvestmentAmount)
        {
            Name = name;
            MinimumInvestmentAmount = minimumInvestmentAmount;
            Investments = new List<Investment>();
        }
        
        private InvestmentOpportunity() { }
    }
}
