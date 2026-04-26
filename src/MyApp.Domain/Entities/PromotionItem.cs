using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyApp.Domain.Entities
{
    public class PromotionItem : BaseEntity<int>
    {
        public int PromotionId { get; private set; }

        public int ProductUnitId { get; private set; }

        public Promotion Promotion { get; private set; } = null!;
        public ProductUnit ProductUnit { get; private set; } = null!;
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




        public static PromotionItem Create(
            int promotionId,
            int productUnitId,
            string productName,
            string unitName,
            decimal originalPrice,
            string? unitBarcode = null,
            decimal? overrideValue = null,
            bool? isPercentageOverride = null)
        {

            if (promotionId <= 0)
                throw new ArgumentException("PromotionId must be valid");
            
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name is required");
            if (string.IsNullOrWhiteSpace(unitName))
                throw new ArgumentException("Unit name is required");
            if (originalPrice < 0)
                throw new ArgumentException("Original price must be >= 0");
            if (overrideValue.HasValue && overrideValue.Value < 0)
                throw new ArgumentException("Override value must be >= 0");


            return new PromotionItem
            {
                PromotionId = promotionId,
                ProductUnitId = productUnitId,
                ProductName = productName.Trim(),
                UnitName = unitName.Trim(),
                OriginalPrice = originalPrice,
                UnitBarcode = unitBarcode?.Trim(),
                OverrideValue = overrideValue,
                IsPercentageOverride = isPercentageOverride
            };

        }


        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void UpdateOriginalPrice(decimal newOriginalPrice)
        {
            if (newOriginalPrice < 0)
                throw new ArgumentException("Original price must be >= 0");
            OriginalPrice = newOriginalPrice;
        }

        public void UpdateOverride(decimal? overrideValue, bool? isPercentageOverride)
        {
            // Clear override
            if (!overrideValue.HasValue)
            {
                OverrideValue = null;
                IsPercentageOverride = null;
                return;
            }

            if (!isPercentageOverride.HasValue)
                throw new ArgumentException("Override phải có loại");

            if (overrideValue < 0)
                throw new ArgumentException("Override value must be >= 0");

            if (isPercentageOverride.Value && overrideValue > 100)
                throw new ArgumentException("Percentage override không được > 100%");

            OverrideValue = overrideValue;
            IsPercentageOverride = isPercentageOverride;
        }

    }
}


