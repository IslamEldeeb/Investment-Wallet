using DinarInvestments.Domain.Shared;

namespace DinarInvestments.Domain.Repositories;

public interface IGenericRepository<T, in TKey> where T : BaseModel<TKey>
    where TKey : struct
{
    Task<T?> GetByIdAsync(TKey id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    Task SaveChangesAsync();
}