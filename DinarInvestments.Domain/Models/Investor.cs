using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models
{
    public class Investor : ModelBase
    {
        public string Name { get; private set; }

        public string Email { get; private set; }
        
        public List<Wallet> Wallets { get; private set; }

        public List<Investment> Investments { get; set; }

        public List<Transaction> Transactions { get; set; }

        private Investor()
        {
            
        }
    }
}

