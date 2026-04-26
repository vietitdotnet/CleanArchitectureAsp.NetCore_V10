using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities
{
    public class Promotion : BaseEntity<int>
    {
        private Promotion() { }

        // Dùng để public ra ngoài, tránh lộ Id auto-increment
        public Guid PublicId { get; private set; }
        public string Name { get; private set; } = null!;

        // Có thể là % hoặc số tiền tùy IsPercentage
        public decimal Value { get; private set; }

        // Nếu là %, sẽ có MaxDiscountAmount để giới hạn số tiền tối đa được giảm
        public bool IsPercentage { get; private set; }

        // Chỉ áp dụng khi IsPercentage = true
        public decimal? MaxDiscountAmount { get; private set; }

        // Thời gian bắt đầu khuyến mãi
        public DateTimeOffset StartDate { get; private set; }

        // Thời gian kết thúc khuyến mãi
        public DateTimeOffset EndDate { get; private set; }

        // Nếu true, chỉ áp dụng cho đơn hàng online
        public bool IsOnlineOnly { get; private set; }

        // Chỉ áp dụng cho FlashSale, giới hạn số lượng được áp dụng khuyến mãi
        public int? LimitQuantity { get; private set; }

        // Số lượng đã bán được áp dụng khuyến mãi, chỉ áp dụng cho FlashSale
        public int SoldQuantity { get; private set; }

        // Chỉ áp dụng cho FlashSale, khung giờ bắt đầu trong ngày
        public TimeSpan? DailyStartTime { get; private set; }

        // Chỉ áp dụng cho FlashSale, khung giờ kết thúc trong ngày
        public TimeSpan? DailyEndTime { get; private set; }

        // Loại khuyến mãi, ví dụ: FlashSale, Seasonal, Clearance...
        public PromotionType Type { get; private set; }

        // Nếu false, khuyến mãi không có hiệu lực dù đang trong khoảng thời gian
        public bool IsActive { get; private set; }

        private readonly List<PromotionItem> _promotionItems = new();
        public IReadOnlyCollection<PromotionItem> PromotionItems => _promotionItems.AsReadOnly();


        public void AddPromotionItem(PromotionItem item)
        {
            if (!IsActive)
                throw new("Khuyến mãi không hoạt động");

            if (StartDate > DateTimeOffset.UtcNow || EndDate < DateTimeOffset.UtcNow)
                throw new InvalidOperationException("Khuyến mãi không nằm trong thời gian áp dụng");

            // tránh duplicate product
            if (_promotionItems.Any(x => x.ProductUnitId == item.ProductUnitId))
                throw new InvalidOperationException("Sản phẩm đã tồn tại trong khuyến mãi");

            _promotionItems.Add(item);
        }

       


        // CHỈ DÙNG CHO PROMOTION THÔNG THƯỜNG
        public static Promotion CreatePromotion(
            string name,
            decimal value,
            bool isPercentage,
            decimal? maxDiscountAmount,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            bool isOnlineOnly = false)
        {
            ValidateCommon(value, isPercentage, maxDiscountAmount, startDate, endDate);

            return new Promotion
            {
                PublicId = Guid.NewGuid(),
                Name = name,
                Value = value,
                IsPercentage = isPercentage,
                MaxDiscountAmount = maxDiscountAmount,
                StartDate = startDate,
                EndDate = endDate,
                Type = PromotionType.Regular, // Gán cứng type
                IsOnlineOnly = isOnlineOnly,
                IsActive = true
            };
        }

        // CHỈ DÙNG CHO FLASH SALE
        public static Promotion CreateFlashSale(
            string name,
            decimal value,
            bool isPercentage,
            decimal? maxDiscountAmount,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            int limitQuantity,
            TimeSpan dailyStart,
            TimeSpan dailyEnd)
        {
            ValidateCommon(value, isPercentage, maxDiscountAmount, startDate, endDate);

            // Validation đặc thù cho Flash Sale
            if (limitQuantity <= 0) throw new InvalidOperationException("FlashSale phải có LimitQuantity > 0");

            return new Promotion
            {
                Name = name,
                Value = value,
                IsPercentage = isPercentage,
                MaxDiscountAmount = maxDiscountAmount,
                StartDate = startDate,
                EndDate = endDate,
                Type = PromotionType.FlashSale,
                LimitQuantity = limitQuantity,
                DailyStartTime = dailyStart,
                DailyEndTime = dailyEnd,
                SoldQuantity = 0,
                IsActive = true
            };
        }
      

        // =========================
        // UPDATE
        // =========================

        public void UpdateBasic(
            string name,
            decimal value,
            bool isPercentage,
            decimal? maxDiscountAmount)
        {
            ValidateCommon(value, isPercentage, maxDiscountAmount, StartDate, EndDate);

            Name = name;
            Value = value;
            IsPercentage = isPercentage;
            MaxDiscountAmount = maxDiscountAmount;
        }

        public void UpdateSchedule(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (endDate <= startDate)
                throw new Exception("EndDate phải lớn hơn StartDate");

            StartDate = startDate;
            EndDate = endDate;
        }

        public void UpdateFlashSale(
            int limitQuantity,
            TimeSpan dailyStart,
            TimeSpan dailyEnd)
        {
            if (Type != PromotionType.FlashSale)
                throw new InvalidOperationException("Không phải FlashSale");

            if (limitQuantity <= 0)
                throw new InvalidOperationException("LimitQuantity phải > 0");

            LimitQuantity = limitQuantity;
            DailyStartTime = dailyStart;
            DailyEndTime = dailyEnd;
        }

        public void SetOnlineOnly(bool isOnlineOnly)
        {
            IsOnlineOnly = isOnlineOnly;
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;


        public bool IsValid(DateTimeOffset now)
        {
            if (!IsActive) return false;
            if (now < StartDate || now > EndDate) return false;

            // Nếu không phải Flash Sale thì tới đây là đủ
            if (Type != PromotionType.FlashSale) return true;

            // Logic đặc thù Flash Sale tách riêng
            return CheckFlashSaleConditions(now);
        }


        public void IncreaseSoldQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Quantity phải > 0");

            if (LimitQuantity.HasValue &&
                SoldQuantity + quantity > LimitQuantity)
                throw new InvalidOperationException("Vượt quá số lượng FlashSale");

            SoldQuantity += quantity;
        }

        // PRIVATE VALIDATION
        private static void ValidateCommon(
            decimal value,
            bool isPercentage,
            decimal? maxDiscountAmount,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            if (value <= 0)
                throw new InvalidOperationException("Value phải > 0");

            if (isPercentage)
            {
                if (value > 100)
                    throw new InvalidOperationException("Phần trăm không được > 100");

                if (maxDiscountAmount is null)
                    throw new InvalidOperationException("Phải có MaxDiscountAmount khi là %");
            }
            else
            {
                if (maxDiscountAmount != null)
                    throw new InvalidOperationException("Không được có MaxDiscountAmount khi là tiền");
            }

            if (endDate <= startDate)
                throw new InvalidOperationException("EndDate phải lớn hơn StartDate");
        }

     
        private bool CheckFlashSaleConditions(DateTimeOffset now)
        {
            // 1. Kiểm tra số lượng
            if (LimitQuantity.HasValue && SoldQuantity >= LimitQuantity) return false;

            // 2. Kiểm tra khung giờ vàng
            if (DailyStartTime.HasValue && DailyEndTime.HasValue)
            {
                var time = now.TimeOfDay;
                if (time < DailyStartTime || time > DailyEndTime) return false;
            }

            return true;
        }
    }



}
