using MyApp.Domain.Paginations.Contants;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Paginations.Parameters
{
    public class ProductParameters : PagingParameters
    {

        private const int MaxKeywordLength = 30;

        private const decimal MinPrice = 0;

        private const decimal MaxPrice = 9999999999;

        public string? KeySearch { get; set; }

        public string? Origin { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        private ProductSort _sortOrder = ProductSort.None;


        public string Sort
        {
            get => _sortOrder.Value;
            set => ProductSort.TryParse(value, out _sortOrder);
        }

        public ProductSort SortOrder => _sortOrder;

        public override void Normalize()
        {
            // Chuẩn hóa KeySearch: loại bỏ khoảng trắng thừa và giới hạn độ dài
            if (!string.IsNullOrWhiteSpace(KeySearch))
            {
                KeySearch = KeySearch.Trim();

                if (KeySearch.Length > MaxKeywordLength)
                {
                    KeySearch = KeySearch.Substring(0, MaxKeywordLength);
                }
            }

            // Nếu PriceFrom có giá trị, đảm bảo nó nằm trong khoảng hợp lệ
            if (PriceFrom.HasValue)
            {
                if (PriceFrom < MinPrice) PriceFrom = MinPrice;
                if (PriceFrom > MaxPrice) PriceFrom = MaxPrice;
            }

            // Nếu PriceTo có giá trị, đảm bảo nó nằm trong khoảng hợp lệ
            if (PriceTo.HasValue)
            {
                if (PriceTo < MinPrice) PriceTo = MinPrice;
                if (PriceTo > MaxPrice) PriceTo = MaxPrice;
            }

            // swap nếu nhập ngược
            if (PriceFrom.HasValue && PriceTo.HasValue && PriceFrom > PriceTo)
            {
                (PriceFrom, PriceTo) = (PriceTo, PriceFrom);
            }
        }
    }
}
