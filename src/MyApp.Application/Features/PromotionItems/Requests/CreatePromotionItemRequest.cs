using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.PromotionItems.Requests
{
    public class CreatePromotionItemRequest
    {       

        public int ProductUnitId { get; init; }

        // giá gốc của sản phẩm tại thời điểm áp dụng khuyến mãi,
        // được lưu lại để đảm bảo tính toán giá chính xác ngay cả khi sau này giá sản phẩm thay đổi
        public decimal OriginalPrice { get; set; }
        // Giá trị này sẽ được ưu tiên sử dụng để tính giá khuyến mãi nếu có,
        // nó có thể là số tiền giảm trực tiếp hoặc phần trăm giảm giá tùy thuộc vào IsPercentageOverride
        public decimal? OverrideValue { get; init; }

        // Nếu IsPercentageOverride = true thì OverrideValue sẽ được hiểu là phần trăm giảm giá,
        // ngược lại sẽ là số tiền giảm giá
        public bool? IsPercentageOverride { get; init; }

    }
}
