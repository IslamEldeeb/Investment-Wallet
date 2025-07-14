using DinarInvestments.Application.Dtos;
using DinarInvestments.Application.Dtos.Investors;

namespace DinarInvestments.Application.Services.Interfaces;

public interface IInvestorService
{
    Task<List<InvestorDto>> GetAllInvestors();
    Task CreateInvestor(CreateInvestorDto input);
    Task UpdateInvestor(long id, UpdateInvestorInfo input);

    Task FundInvestorWallet(FundInvestorWallet input);

}