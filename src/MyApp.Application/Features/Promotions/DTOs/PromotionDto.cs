using MyApp.Application.Features.Promotions.MapRaws;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.DTOs
{
    public class PromotionDto : BaseDto
    {
        public int Id { get; set; }

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

    }
}
