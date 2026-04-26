using MediatR;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.MapRaws
{
    public class PromotionItemRaw : BaseDto
    {
        public int PromotionId { get; private set; }

        public int ProductUnitId { get; private set; }

        public string ProductName { get; set; } = null!;

        public string UnitName { get; private set; } = null!;

        public string? UnitBarcode { get; private set; }

        public decimal? OverrideValue { get; set; }

        public bool? IsPercentageOverride { get; set; }

        public bool IsActive { get; set; }

        public decimal? MaxDiscountAmount { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public decimal Value { get; set; }

        public bool IsPercentage { get; set; }

        public Guid PublicId { get; set; }

        public string Name { get; set; } = null!;

        // Mở rộng cho Flash Sale / Online Only
        public bool IsOnlineOnly { get; set; } // Chỉ áp dụng Online

        // Field dành riêng cho Flash Sale (Nullable)
        public int? LimitQuantity { get; set; } // Tổng số lượng bán trong đợt flash sale
        public int SoldQuantity { get; set; } // Số lượng đã bán thực tế

        // Lưu khung giờ vàng: ví dụ "08:00:00" và "22:00:00"
        public TimeSpan? DailyStartTime { get; set; }
        public TimeSpan? DailyEndTime { get; set; }

        public PromotionType Type { get; set; } // Standard, FlashSale...

    }
}
