using DinarInvestments.Domain.Models;
using DinarInvestments.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DinarInvestments.Infrastructure.Repositories;

public class InvestorRepository(InvestorDbContext context)
    : GenericRepository<Investor, long>(context), IInvestorRepository
{
    public async Task<Investor?> GetInvestorAsync(long id)
    {
        return await DbSet.Include(x => x.Wallets)
            .AsTracking()
            .FirstOrDefaultAsync(e => e.Id.Equals(id));
    }
}