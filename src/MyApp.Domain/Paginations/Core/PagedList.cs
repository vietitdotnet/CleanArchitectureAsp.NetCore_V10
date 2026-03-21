
namespace MyApp.Domain.Paginations.Core
{
    public class PagedList<T>
    {
        public IReadOnlyList<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);


        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PagedList(IList<T> values, int count, int pageIndex, int pageSize)
        {
            Items = values.ToList();
            TotalCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;

        }
        public PagedList(IQueryable<T> values, int count, int pageIndex, int pageSize)
        {
            Items = values.ToList();
            TotalCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;

        }

    }
}
