using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models
{
    public class Investment : ModelBase
    {
        public long InvestorId { get; private set; }
        public long InvestmentOpportunityId { get; private set; }
        public decimal Amount { get; private set; }

        private Investment()
        {
        }
    }
}