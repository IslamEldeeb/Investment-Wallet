namespace DinarInvestments.Domain.Models.ValueObjects
{
    public class Investment
    {
        public Guid InvestorId { get; private set; }
        public Guid InvestmentOpportunityId { get; private set; }

        public decimal Amount { get; private set; }

        public DateTime DateTime { get; private set; }

        private Investment()
        {
        }
    }
}