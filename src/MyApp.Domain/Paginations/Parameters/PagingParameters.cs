namespace MyApp.Domain.Paginations.Parameters
{
    public class PagingParameters
    {
        private const int MaxPageSize = 1000;
        private int _pageIndex = 1;
        private int _pageSize = 12;

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value switch
            {
                <= 0 => 0,
                > MaxPageSize => MaxPageSize,
                _ => value
            };
        }

        public virtual void Normalize() { }
    }
}
