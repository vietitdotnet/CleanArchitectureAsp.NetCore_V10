using MyApp.Domain.Core.Models;
using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using System.Linq.Expressions;

namespace MyApp.Domain.Core.Repositories
{
    public interface IReadRepository<T, TKey>
    where TKey : notnull
    where T : BaseEntity<TKey> 
    {
        Task<T?>GetByIdAsync(TKey id, CancellationToken ct = default);

        Task<TDto?> GetByIdProjectedAsync<TDto>(TKey id, CancellationToken ct = default) where TDto : BaseDto;

        Task<T?> FirstOrDefaultAsync(ISpecification<T> spec , CancellationToken ct = default);

        Task<TDto?> FirstOrDefaultProjectedAsync<TDto>(ISpecification<T> spec, CancellationToken ct = default) where TDto : BaseDto;

        Task<TDto?> FirstOrDefaultProjectedAsync<TDto>(ISpecification<T> spec, DateTimeOffset now = default, CancellationToken ct = default) where TDto : BaseDto;

        Task<TResult?> FirstOrDefaultWidthSelectorAsync<TResult>(ISpecification<T> spec, Expression<Func<T, TResult>> selector, CancellationToken ct = default);
        
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken ct = default);
        Task<IReadOnlyList<TDto>> ListProjectedAsync<TDto>(ISpecification<T> spec, CancellationToken ct = default) where TDto : BaseDto;
        Task<int> CountAsync(ISpecification<T> spec, CancellationToken ct = default);

        Task<PagedResponse<TDto, TQuery>> GetPagedAsync<TDto, TQuery>(ISpecification<T> spec, TQuery pagingParams, CancellationToken ct = default)
        where TDto : BaseDto
        where TQuery : PagingParameters;
    }
}
