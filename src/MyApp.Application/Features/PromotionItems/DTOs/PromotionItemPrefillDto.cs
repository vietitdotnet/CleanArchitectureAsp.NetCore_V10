using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.PromotionItems.DTOs
{
    public class PromotionItemPrefillDto : BaseDto
    {
        public int ProductUnitId { get; set; }
        public string ProductName { get; set; } = null!;
        public string UnitName { get; set; } = null!;
        public string? UnitBarcode { get; set; }
        public decimal OriginalPrice { get; set; }

        // default gợi ý
        public decimal? OverrideValue { get; set; }
        public bool? IsPercentage { get; set; }
    }
}
