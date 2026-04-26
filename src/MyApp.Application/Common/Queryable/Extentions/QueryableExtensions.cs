using MyApp.Domain.Core.Models;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Exceptions;
using System.Linq.Expressions;


namespace MyApp.Application.Common.Queryable.Extentions
{
    public static class QueryableExtensions
    {
       

        public static async Task<bool> ExistsAsync<TEntity, TKey>(
            this IBaseRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> predicate)
            where TEntity : BaseEntity<TKey>
            where TKey : notnull
        {
            var spec = new BaseSpecification<TEntity>(predicate);

            var entity = await repository.FirstOrDefaultAsync(spec);

            return entity != null;
        }

        public static async Task EnsureExistsAsync<TEntity, TKey>(
            this IBaseRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> predicate,
            string? message = null)
            where TEntity : BaseEntity<TKey>
            where TKey : notnull
        {
            var exists = await repository.ExistsAsync(predicate);

            if (!exists)
                throw new NotFoundException(
                    message ?? $"{typeof(TEntity).Name} not found");
        }

    }

}
