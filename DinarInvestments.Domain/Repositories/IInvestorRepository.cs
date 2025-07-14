using DinarInvestments.Domain.Models;

namespace DinarInvestments.Domain.Repositories;

public interface IInvestorRepository : IGenericRepository<Investor, long>
{
    Task<Investor> GetInvestorAsync(long id);
}