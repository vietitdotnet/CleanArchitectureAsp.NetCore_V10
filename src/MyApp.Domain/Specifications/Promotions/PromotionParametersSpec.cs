using MyApp.Domain.Entities;
using MyApp.Domain.Paginations.Contants;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyApp.Domain.Specifications.Promotions
{
    public class PromotionParametersSpec : PromotionSpec
    {
        private bool _isSorted;
        public PromotionParametersSpec(PromotionParameters p)
        {
            Criteria = BuildCriteria(p);
            ApplySortingStartDate(p);
        }

        private static Expression<Func<Promotion, bool>> BuildCriteria(PromotionParameters p)
        {
            Expression<Func<Promotion, bool>> expr = x => true;
            
            // Lọc theo tên (nếu có)
            //....
            
            return expr;
        }

        private void ApplySortingStartDate(PromotionParameters p)
        {
            if (_isSorted || p.SortPromotion.IsNone)
                return;

            var applied = p.SortPromotion switch
            {
                var s when s == PromotionSort.StartDateAsc => ApplyStartDateAsc(),
                var s when s == PromotionSort.StartDateDesc => ApplyStartDateDesc(),
                _ => false
            };

            if (applied)
                _isSorted = true;

        }

        private bool ApplyStartDateAsc()
        {
            ApplyOrderBy(x => x.StartDate);
            return true;
        }

        private bool ApplyStartDateDesc()
        {
            ApplyOrderByDescending(x => x.StartDate);
            return true;
        }

    }
}
