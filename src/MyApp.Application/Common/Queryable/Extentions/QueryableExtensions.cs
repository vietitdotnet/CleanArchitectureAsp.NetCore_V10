using MyApp.Domain.Core.Models;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Exceptions;
using System.Linq.Expressions;


namespace MyApp.Application.Common.Queryable.Extentions
{
    public static class QueryableExtensions
    {
        public static async Task<TKey> GetIdBySlugAsync<TEntity, TKey>(
             this IBaseRepository<TEntity, TKey> repository,
             string slug)
             where TEntity : BaseEntity<TKey>, IHasSlug
             where TKey : notnull
        {
            var spec = new BaseSpecification<TEntity>(x => x.Slug == slug);

            var id = await repository.FirstOrDefaultAsync(spec, x => x.Id);

            if (EqualityComparer<TKey>.Default.Equals(id, default!))
                throw new NotFoundException(
                    $"{typeof(TEntity).Name} with slug '{slug}' not found");

            return id!;
        }

        public static async Task<TEntity> GetByIdOrThrowAsync<TEntity, TKey>(
            this IBaseRepository<TEntity, TKey> repository,
            TKey id)
            where TEntity : BaseEntity<TKey>
            where TKey : notnull
        {
            var entity = await repository.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException($"{typeof(TEntity).Name} with id '{id}' not found");

            return entity;
        }

        public static async Task<TEntity> GetBySlugAsync<TEntity, TKey>(
            this IBaseRepository<TEntity, TKey> repository,
            string slug)
            where TEntity : BaseEntity<TKey>, IHasSlug
            where TKey : notnull
        {
            var spec = new BaseSpecification<TEntity>(x => x.Slug == slug);

            var entity = await repository.FirstOrDefaultAsync(spec);

            if (entity == null)
                throw new NotFoundException($"{typeof(TEntity).Name} with slug '{slug}' not found");

            return entity;
        }

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
