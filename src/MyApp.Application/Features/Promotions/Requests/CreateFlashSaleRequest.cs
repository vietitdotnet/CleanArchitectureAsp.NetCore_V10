using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.Requests
{
    public class CreateFlashSaleRequest : CreatePromotionBase
    {
        public int LimitQuantity { get; set; }
        public TimeSpan DailyStartTime { get; set; }
        public TimeSpan DailyEndTime { get; set; }
    }
}
