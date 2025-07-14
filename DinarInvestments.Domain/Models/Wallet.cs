namespace DinarInvestments.Domain.Models
{
    public class Wallet 
    {
        public long Id { get; set; }
        public long InvestorId { get; private set; }
        public decimal Balance { get; private set; }

        public Wallet(long investorId)
        {
            InvestorId = investorId;
            Balance = 0;
        }

        private Wallet() { }

        public void Fund(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            Balance += amount;
        }
    }
}
