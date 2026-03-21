using MyApp.Domain.Core.Models;

namespace MyApp.Domain.Core.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task RollBackChangesAsync(CancellationToken ct =default);    
        IBaseRepository<T, TKey> Repository<T, TKey>() 
            where T : BaseEntity<TKey> 
            where TKey : notnull;
    }
}