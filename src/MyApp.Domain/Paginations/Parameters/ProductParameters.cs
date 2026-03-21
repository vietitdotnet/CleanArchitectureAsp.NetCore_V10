
using MyApp.Domain.Contants;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Paginations.Parameters
{
    public class ProductParameters : PagingParameters
    {
        [StringLength(30)]
        public string? KeySearch { get; set; }

        public string? Origin { get; set; }

        [Range(0, 9999999999)]
        public decimal? PriceFrom { get; set; }

        [Range(0, 9999999999)]
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
            base.Normalize();

            KeySearch = string.IsNullOrWhiteSpace(KeySearch) ? null : KeySearch.Trim();
            Origin = string.IsNullOrWhiteSpace(Origin) ? null : Origin.Trim();

            if (PriceFrom.HasValue && PriceTo.HasValue && PriceFrom > PriceTo)
            {
                (PriceFrom, PriceTo) = (PriceTo, PriceFrom);
            }
        }
    }
}
