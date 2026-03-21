using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Specifications.Base;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Entities;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Infrastructure.Data;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyApp.Infrastructure.Repositories
{
    public class BaseRepository<T, TKey> : IBaseRepository<T, TKey>
        where T : BaseEntity<TKey>   
        where TKey : notnull

    {
        protected readonly MyAppDbContext _dbContext;
        protected readonly IConfigurationProvider _mapperConfig;

        protected readonly DbSet<T> _dbSet;


        public BaseRepository(MyAppDbContext dbContext, IConfigurationProvider mapperConfig)
        {
            _dbContext = dbContext;
            _mapperConfig = mapperConfig;
            _dbSet = _dbContext.Set<T>();

        }

        public async Task<T?> GetByIdAsync(TKey id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, ct);
        }

        public async Task<TDto?> GetByIdProjectedAsync<TDto>(TKey id, CancellationToken ct = default)
            where TDto : BaseDto
        {
            return await _dbSet
                   .Where(e => e.Id!.Equals(id))
                   .ProjectTo<TDto>(_mapperConfig)
                   .AsNoTracking()
                   .FirstOrDefaultAsync(ct);
        }

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            return await ApplySpecification(spec)
                         .FirstOrDefaultAsync(ct);
        }

        public async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T> spec,Expression<Func<T, TResult>> selector, CancellationToken ct = default)
        {
            return await ApplySpecification(spec)
                .Select(selector)
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            return await ApplySpecification(spec).ToListAsync(ct);
        }

        public async Task<IReadOnlyList<TDto>> ListProjectedAsync<TDto>(ISpecification<T> spec, CancellationToken ct = default)
           where TDto : BaseDto
        {
            var query = ApplySpecification(spec).AsNoTracking();

            return await query
                .ProjectTo<TDto>(_mapperConfig)
                .ToListAsync(ct);
        }

     
        public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            return await ApplySpecification(spec).CountAsync(ct);
        }

        public async Task<PagedResponse<TDto, TQuery>> GetPagedAsync<TDto, TQuery>
            (ISpecification<T> spec, TQuery pagingParams, CancellationToken ct = default)
            where TDto : BaseDto
            where TQuery : PagingParameters
        {
            var query = ApplySpecification(spec);
            var totalCount = await query.CountAsync(ct);

            var items = await query
                .ProjectTo<TDto>(_mapperConfig)
                .ToListAsync();

            return new PagedResponse<TDto, TQuery>(items, totalCount, pagingParams.PageIndex, pagingParams.PageSize)
            {
                Query = pagingParams
            };
        }

        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
            return entity;
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return _dbContext.SaveChangesAsync(ct);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
        }

    }

}
