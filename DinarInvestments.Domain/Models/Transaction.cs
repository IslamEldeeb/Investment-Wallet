using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models
{
    public class Transaction : ModelBase
    {
        public long InvestorId { get; private set; }
        public long FromWalletId { get; private set; }
        public long ToWalletId { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public string TransactionCode { get; set; }

        private Transaction()
        {
        }
    }
}