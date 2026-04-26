using MyApp.Domain.Paginations.Contants;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Paginations.Parameters
{
    public class PromotionParameters : PagingParameters
    {

        protected override int DefaultPageSize => 5;
        
        private PromotionSort _sort = PromotionSort.StartDateDesc;

        public string Sort
        {
            get => _sort.Value;
            set => PromotionSort.TryParse(value, out _sort);
        }
        public PromotionSort SortPromotion => _sort;

        public override void Normalize()
        {
            // Không có trường nào cần chuẩn hóa trong PromotionParameters hiện tại
        }
    }
}
