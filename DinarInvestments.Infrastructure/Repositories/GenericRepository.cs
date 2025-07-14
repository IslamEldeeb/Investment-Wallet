using DinarInvestments.Domain.Repositories;
using DinarInvestments.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace DinarInvestments.Infrastructure.Repositories;

public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : BaseModel<TKey>
    where TKey : struct
{
    protected readonly InvestorDbContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(InvestorDbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(TKey id)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}