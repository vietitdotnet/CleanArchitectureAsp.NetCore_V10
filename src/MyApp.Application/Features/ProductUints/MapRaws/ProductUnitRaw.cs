using MyApp.Application.Features.Promotions.MapRaws;
using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.ProductUints.MapRaws
{
    public class ProductUnitRaw : BaseDto
    {    
        public int ProductId { get;  set; }
        public Guid PublicId { get;  set; }
        public string? Barcode { get;  set; }
        public string UnitName { get;  set; } = null!;
        public int ConversionRate { get;  set; } //Ví dụ: 1 thùng = 24 đơn vị base
        public decimal SellingPrice { get;  set; } /// Đơn vị gốc (chai, cái...)
        public bool IsBaseUnit { get;  set; }
        public int StockQuantity { get; private set; }
        public bool IsOutOfStock => StockQuantity <= 0;
        public List<PromotionItemRaw> PromotionRawItems { get; set; } = new List<PromotionItemRaw>();

    }
}
