using MyApp.Application.Features.Promotions.MapRaws;
using MyApp.Domain.Entities;


namespace MyApp.Application.Features.Promotions.LogicFeatures
{
    // Chiến lược cho Flash Sale
    public class FlashSaleStrategy : IPromotionStrategy
    {
        public bool CanApply(PromotionItemRaw p, DateTimeOffset now)
        {
            // 1. Kiểm tra thời gian tổng quát và trạng thái
            if (!p.IsActive || now < p.StartDate || now > p.EndDate) return false;

            // 2. Kiểm tra số lượng (Logic từ CK_Promotions_Quantity_Logic)
            if (p.LimitQuantity.HasValue && p.SoldQuantity >= p.LimitQuantity.Value) return false;

            // 3. Kiểm tra khung giờ hàng ngày (Logic từ CK_Promotions_DailyTime_Logic)
            if (p.DailyStartTime.HasValue && p.DailyEndTime.HasValue)
            {
                var currentTime = now.TimeOfDay;
                if (currentTime < p.DailyStartTime.Value || currentTime > p.DailyEndTime.Value)
                    return false;
            }

            return true;
        }

        public decimal Calculate(decimal sellingPrice, PromotionItemRaw p)
        {
            var value = p.OverrideValue ?? p.Value;
            var isPercent = p.IsPercentageOverride ?? p.IsPercentage;

            // Flash Sale thường giảm thẳng theo giá trị, ít khi chặn MaxAmount nhưng vẫn có thể thêm nếu cần
            return isPercent ? sellingPrice * (value / 100m) : value;
        }
    }
}
