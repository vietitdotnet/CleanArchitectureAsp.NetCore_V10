using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.DTOs
{
    public class FlashSalePromotionDto : BaseDto
    {
        public string PublicId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Value { get; set; }
        public bool IsPercentage { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        // Các trường đặc thù của Flash Sale
        public int LimitQuantity { get; set; }
        public int SoldQuantity { get; set; }
        public TimeSpan DailyStartTime { get; set; }
        public TimeSpan DailyEndTime { get; set; }

        // Bổ sung các helper field giúp Frontend dễ hiển thị
        public bool IsSoldOut => SoldQuantity >= LimitQuantity;
        public double SoldPercentage => Math.Round((double)SoldQuantity / LimitQuantity * 100, 2);

        public bool IsActive { get; set; }
        public string TextType {  get; set; } = null!;
    }
}
