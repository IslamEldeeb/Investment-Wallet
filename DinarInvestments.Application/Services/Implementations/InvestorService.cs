using DinarInvestments.Application.Dtos;
using DinarInvestments.Application.Dtos.Investors;
using DinarInvestments.Application.Services.Interfaces;
using DinarInvestments.Domain.Models;
using DinarInvestments.Domain.Repositories;
using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Application.Services.Implementations;

public class InvestorService(IInvestorRepository investorRepository)
    : IInvestorService
{
    public async Task<List<InvestorDto>> GetAllInvestors()
    {
        var investors = await investorRepository.GetAllAsync();
        return investors.Select(i => new InvestorDto
        {
            Id = i.Id,
            Name = i.Name,
            Email = i.Email
        }).ToList();
    }

    public async Task CreateInvestor(CreateInvestorDto input)
    {
        Guard.AssertArgumentNotNull(input, nameof(input));

        var investor = Investor.Create(input.Name, input.Email);
        await investorRepository.AddAsync(investor);
        await investorRepository.SaveChangesAsync();
    }

    public async Task UpdateInvestor(long id, UpdateInvestorInfo input)
    {
        Guard.AssertArgumentNotNull(input, nameof(input));
        Guard.AssertArgumentNotLessThanOrEqualToZero<long>(id, nameof(id));

        var investor = await investorRepository.GetByIdAsync(id);
        if (investor == null)
            throw new InvalidOperationException($"Investor with ID {id} not found.");

        investor.UpdateInfo(input.Name, input.Email);
        await investorRepository.SaveChangesAsync();
    }

    public async Task FundInvestorWallet(FundInvestorWallet input)
    {
        Guard.AssertArgumentNotNull(input, nameof(input));
        Guard.AssertArgumentNotLessThanOrEqualToZero<long>(input.InvestorId, nameof(input.InvestorId));
        Guard.AssertArgumentNotLessThanOrEqualToZero<decimal>(input.Amount, nameof(input.Amount));

        var investor = await investorRepository.GetInvestorAsync(input.InvestorId);
        if (investor == null)
            throw new InvalidOperationException($"Investor with ID {input.InvestorId} not found.");

        investor.FundMainWallet(input.Amount, input.CorrelationId);
        // investorRepository.Update(investor);
        await investorRepository.SaveChangesAsync();
    }

    public async Task<Decimal> GetBalance(long investorId)
    {
        Guard.AssertArgumentNotLessThanOrEqualToZero<long>(investorId, nameof(investorId));

        var investor = await investorRepository.GetInvestorAsync(investorId);
        if (investor == null)
            throw new InvalidOperationException($"Investor with ID {investorId} not found.");

        return investor.GetMainWalletBalance();
    }
}