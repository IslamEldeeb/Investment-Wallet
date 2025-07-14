using System.ComponentModel.DataAnnotations;

namespace DinarInvestments.Application.Dtos.Investors;

public class FundInvestorWallet
{
    [Required] public long InvestorId { get; set; }

    [Required] public decimal Amount { get; set; }
    
    [Required] public string CorrelationId { get; set; }
}