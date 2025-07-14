using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models
{
    public class Transaction : AggregateRoot
    {
        public Guid FromWalletId { get; private set; }
        public Guid ToWalletId { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public DateTime TransactionDate { get; private set; }

        private Transaction() { }

       
    }
}