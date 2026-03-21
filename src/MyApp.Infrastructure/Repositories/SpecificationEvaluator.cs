using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Core.Specifications;


namespace MyApp.Application.Specifications.Base
{
    public static class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            // Filtering
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            // Sorting
            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            // Grouping
            if (spec.GroupBy != null)
                query = query.GroupBy(spec.GroupBy).SelectMany(x => x);

            // Includes (expression-based)
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            // Includes (string-based)
            query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            // Paging
            if (spec.IsPagingEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            // Tracking
            if (spec.AsNoTracking)
                query = query.AsNoTracking();

            return query;
        }
    }
}
