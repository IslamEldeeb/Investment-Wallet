using DinarInvestments.Domain.Models.ValueObjects;
using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Models
{
    public class Investor : AggregateRoot
    {
        public string Name { get; private set; }

        public string Email { get; private set; }
        
        public List<Wallet> Wallets { get; private set; }
        public List<Investment> Investments { get; private set; }
        
    }
}

