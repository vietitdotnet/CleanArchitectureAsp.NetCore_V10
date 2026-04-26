using MyApp.Domain.Core.Models;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.PromotionItems.DTOs
{
    public class PromotionItemDto : BaseDto
    {
        public int Id { get; private set; }

        public int PromotionId { get; private set; }

        public int ProductUnitId { get; private set; }

        public string ProductName { get; private set; } = null!;

        public string UnitName { get; private set; } = null!;

        public string? UnitBarcode { get; private set; }

        // giá gốc của sản phẩm tại thời điểm áp dụng khuyến mãi,
        // được lưu lại để đảm bảo tính toán giá chính xác ngay cả khi sau này giá sản phẩm thay đổi
        public decimal OriginalPrice { get; private set; }

        // Giá trị này sẽ được ưu tiên sử dụng để tính giá khuyến mãi nếu có,
        // nó có thể là số tiền giảm trực tiếp hoặc phần trăm giảm giá tùy thuộc vào IsPercentageOverride
        public decimal? OverrideValue { get; private set; }


        // Nếu IsPercentageOverride = true thì OverrideValue sẽ được hiểu là phần trăm giảm giá,
        // ngược lại sẽ là số tiền giảm giá
        public bool? IsPercentageOverride { get; private set; }

        // Cờ này cho phép kích hoạt hoặc tạm ngưng khuyến mãi mà không cần xóa bản ghi,
        // giúp dễ dàng quản lý và theo dõi lịch sử khuyến mãi
        public bool IsActive { get; private set; } = true;

        public string FullProductUnitName => $"{ProductName} - {UnitName}";
    }
}
