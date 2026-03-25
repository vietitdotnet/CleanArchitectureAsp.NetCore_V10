using AutoMapper;
using AutoMapper.QueryableExtensions;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using Microsoft.EntityFrameworkCore;


namespace MyApp.Infrastructure.Common.Queryable.Extentions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResponse<TDto, TQuery>> ApplyPagingWithProjection<TEntity, TDto, TQuery>(
            this IQueryable<TEntity> query,
            TQuery pagingParams,
            IConfigurationProvider mapperConfig)
            where TEntity : class
            where TQuery : PagingParameters
        {
            var projected = query.ProjectTo<TDto>(mapperConfig);

            var count = await projected.CountAsync();
            var items = await projected
                .Skip((pagingParams.PageIndex - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync();


            return new PagedResponse<TDto, TQuery>(items, count, pagingParams.PageIndex, pagingParams.PageSize) { Query = pagingParams };
        }
        

    }
}
