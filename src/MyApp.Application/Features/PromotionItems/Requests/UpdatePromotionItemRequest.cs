using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.PromotionItems.Requests
{
    public class UpdatePromotionItemRequest
    {
        public decimal OriginalPrice { get; set; }
        // Giá trị này sẽ được ưu tiên sử dụng để tính giá khuyến mãi nếu có,
        // nó có thể là số tiền giảm trực tiếp hoặc phần trăm giảm giá tùy thuộc vào IsPercentageOverride
        public decimal? OverrideValue { get; init; }

        // Nếu IsPercentageOverride = true thì OverrideValue sẽ được hiểu là phần trăm giảm giá,
        // ngược lại sẽ là số tiền giảm giá
        public bool? IsPercentageOverride { get; init; }
    }
}
