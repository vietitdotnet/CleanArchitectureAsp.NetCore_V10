using MyApp.Domain.Entities;
using MyApp.Domain.Extentions;
using MyApp.Domain.Paginations.Contants;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Domain.Specifications.Products;
using System.Linq.Expressions;

namespace MyApp.Application.Specifications.Products
{
    public class ProductParametersSpec : ProductSpec
    {
        private bool _isSorted;

        public ProductParametersSpec(ProductParameters p)
        {
            Criteria = BuildCriteria(p);

            ApplySortingPrice(p);

            ApplySortingDateCreate(p);
        }

       
        private static Expression<Func<Product, bool>> BuildCriteria(ProductParameters p)
        {
            Expression<Func<Product, bool>> expr = x => true;

            if (!string.IsNullOrWhiteSpace(p.KeySearch))
            {
                var search = p.KeySearch.Trim();
                expr = expr.And(x => x.Name.Contains(search));
            }

            if (p.PriceFrom.HasValue)
                expr = expr.And(x => x.CostPrice >= p.PriceFrom.Value);

            if (p.PriceTo.HasValue)
                expr = expr.And(x => x.CostPrice <= p.PriceTo.Value);

            return expr;
        }

        private void ApplySortingPrice(ProductParameters p)
        {
            if (_isSorted || p.SortOrder.IsNone)
                return;

            var applied = p.SortOrder switch
            {
                var s when s == ProductSort.PriceAsc => ApplyPriceAsc(),               
                var s when s == ProductSort.PriceDesc => ApplyPriceDesc(),
                _ => false
            };

            if (applied)
                _isSorted = true;
        }

       
        private void ApplySortingDateCreate(ProductParameters p)
        {
            if (_isSorted || p.SortOrder.IsNone)
                return;

            var applied = p.SortOrder switch
            {
                var s when s == ProductSort.DateAsc => ApplyDateAsc(),
                var s when s == ProductSort.DateDesc => ApplyDateDesc(),
                _ => false
            };

            if (applied)
                _isSorted = true;

        }

        private bool ApplyPriceAsc()
        {
            ApplyOrderBy(x => x.CostPrice);
            return true;
        }

        private bool ApplyPriceDesc()
        {
            ApplyOrderByDescending(x => x.CostPrice);
            return true;
        }

        private bool ApplyDateAsc()
        {
            ApplyOrderBy(x => x.DateCreate);
            return true;
        }

        private bool ApplyDateDesc()
        {
            ApplyOrderByDescending(x => x.DateCreate);
            return true;
        }

    }
}
