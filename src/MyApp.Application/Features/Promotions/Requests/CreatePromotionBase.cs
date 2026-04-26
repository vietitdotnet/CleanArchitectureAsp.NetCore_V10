using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.Requests
{
    public class CreatePromotionBase
    {
        public string Name { get; set; } = null!;
        public decimal Value { get; set; }
        public bool IsPercentage { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsOnlineOnly { get; set; }
    }
}
