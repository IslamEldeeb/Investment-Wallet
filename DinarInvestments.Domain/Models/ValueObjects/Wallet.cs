namespace DinarInvestments.Domain.Models.ValueObjects
{
    public class Wallet
    {
        public Guid InvestorId { get; private set; }
        public decimal Balance { get; private set; }

        public Wallet(Guid investorId)
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
