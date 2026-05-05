using MyApp.Domain.Paginations.Interfaces;

namespace MyApp.Domain.Paginations.Parameters
{
    public abstract class PagingParameters : INormalizable
    {
        private const int MaxPageSize = 1000;
        protected virtual int MaxKeywordLength => 20;

        private int _pageIndex = 1;
        private int _pageSize;

        protected virtual int DefaultPageSize => 12;

        public string? KeySearch { get; set; }

        public PagingParameters()
        {
            _pageSize = DefaultPageSize;
        }
        
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
                <= 0 => DefaultPageSize,
                > MaxPageSize => MaxPageSize,
                _ => value
            };
        }

        public virtual void Normalize()
        {
            if (!string.IsNullOrWhiteSpace(KeySearch))
            {
                KeySearch = KeySearch.Trim();

                if (KeySearch.Length > MaxKeywordLength)
                {
                    KeySearch = KeySearch.Substring(0, MaxKeywordLength);
                }
            }
        }
    }

}
