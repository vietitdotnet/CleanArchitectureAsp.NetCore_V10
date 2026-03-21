
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.Domain.Paginations.Core
{
    public class PagedResponse<T, TQuery> : PagedList<T>
        where TQuery : PagingParameters
    {
        public TQuery? Query { get; set; }

        public static class PagingDefaults
        {
            public const int DefaultPageIndex = 1;
            public const int DefaultPageSize = 12;
            public const int MaxPageSize = 100;
        }


        public PagedResponse
                (IQueryable<T> values,
                int totalCount,
                int pageIndex,
                int pageSize)
            : base(values, totalCount, pageIndex, pageSize)
        {
        }

        public PagedResponse
        (IList<T> values,
         int totalCount,
         int pageIndex,
         int pageSize)
             : base(values, totalCount, pageIndex, pageSize)
        {
        }

       
    }
} 