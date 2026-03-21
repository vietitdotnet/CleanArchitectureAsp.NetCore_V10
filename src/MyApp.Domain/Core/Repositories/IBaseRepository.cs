
using MyApp.Domain.Core.Models;

namespace MyApp.Domain.Core.Repositories
{
    public interface IBaseRepository<T, TKey> : IReadRepository<T, TKey> 
        where TKey : notnull
        where T : BaseEntity<TKey> 
    {
        Task<T> AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        
    }


    
}
