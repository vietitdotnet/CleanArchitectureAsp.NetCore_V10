
using MyApp.Domain.Core.Models;
using MyApp.Domain.Core.Specifications;

namespace MyApp.Domain.Core.Repositories
{
    public interface IBaseRepository<T, TKey> : IReadRepository<T, TKey> 
        where TKey : notnull
        where T : BaseEntity<TKey> 
    {

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);
        Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken ct = default);
        Task<T> AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        
    }


    
}
