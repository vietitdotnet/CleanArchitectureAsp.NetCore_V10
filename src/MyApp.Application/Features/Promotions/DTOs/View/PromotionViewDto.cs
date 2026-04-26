using MyApp.Application.Features.Promotions.MapRaws;


namespace MyApp.Application.Features.Promotions.DTOs.View
{
    public class PromotionViewDto
    {    
        public string PublicId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsPercentage { get; set; }
        public decimal Value { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsActive => IsValid(DateTimeOffset.UtcNow);

        // Mở rộng cho Flash Sale / Online Only
        public bool IsOnlineOnly { get; set; } // Chỉ áp dụng Online

        // Field dành riêng cho Flash Sale (Nullable)
        public int? LimitQuantity { get; set; } // Tổng số lượng bán trong đợt flash sale
        public int SoldQuantity { get; set; } // Số lượng đã bán thực tế

        // Lưu khung giờ vàng: ví dụ "08:00:00" và "22:00:00"
        public TimeSpan? DailyStartTime { get; set; }
        public TimeSpan? DailyEndTime { get; set; }
        public string TextType { get; set; } = null!; // Chuỗi hiển thị loại khuyến mãi (ví dụ: "Standard", "Flash Sale")
        public int Type { get; set; } // Loại khuyến mãi: Standard, FlashSale...
        public bool IsValid(DateTimeOffset now)
        {
            return now >= StartDate && now <= EndDate;
        }
        public static PromotionViewDto Create(

            string publicId,
            string name,
            decimal value,
            bool isPercentage,
            decimal? maxDiscountAmount,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            int type,
            string textType,
            bool isOnlineOnly = false,
            int? limitQuantity = null,
            TimeSpan? dailyStartTime = null,
            TimeSpan? dailyEndTime = null)

        {
            return new PromotionViewDto
            {
                PublicId = Guid.NewGuid().ToString(), // Tạo PublicId mới
                Name = name,
                Value = value,
                IsPercentage = isPercentage,
                MaxDiscountAmount = maxDiscountAmount,
                StartDate = startDate,
                EndDate = endDate,
                IsOnlineOnly = isOnlineOnly,
                LimitQuantity = limitQuantity,
                SoldQuantity = 0, // Mặc định chưa bán được sản phẩm nào
                DailyStartTime = dailyStartTime,
                DailyEndTime = dailyEndTime,
                Type = type,
                TextType = textType,
            };
        }

        public static PromotionViewDto From(PromotionItemRaw raw)
        {
            return new PromotionViewDto
            {
                // Sử dụng PublicId để trả về cho Client
                PublicId = raw.PublicId.ToString(),
                Name = raw.Name,
                Value = raw.OverrideValue ?? raw.Value,
                IsPercentage = raw.IsPercentageOverride ?? raw.IsPercentage,
                MaxDiscountAmount = raw.MaxDiscountAmount,
                StartDate = raw.StartDate,
                EndDate = raw.EndDate,
                IsOnlineOnly = raw.IsOnlineOnly,
                LimitQuantity = raw.LimitQuantity,
                SoldQuantity = raw.SoldQuantity,
                DailyStartTime = raw.DailyStartTime,
                DailyEndTime = raw.DailyEndTime,
                Type = (int)raw.Type,
                TextType = raw.Type.ToString(),
            };
        }

    }
}
