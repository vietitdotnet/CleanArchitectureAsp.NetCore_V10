using MyApp.Application.Features.Promotions.DTOs;
using MyApp.Application.Features.Promotions.DTOs.View;
using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyApp.Application.Features.ProductUints.DTOs.View
{
    public class ProductUnitViewDto : BaseDto
    {
        public string PublicId { get; private set; } = null!;

        public string UnitName { get; private set; } = null!;

        public string? Barcode { get; private set; }

        // giá gốc hiện tại
        public decimal OriginalPrice { get; private set; }

        // giá sau khi apply promotion
        public decimal FinalPrice { get; private set; }

        // số tiền giảm

        public decimal? DiscountValue { get; private set; }
        
        public bool IsDiscounted => DiscountValue.HasValue && DiscountValue > 0;

        public bool IsBaseUnit { get; set; }

       //public int StockQuantity { get; private set; }

       // public bool IsOutOfStock => StockQuantity <= 0;

        public IList<PromotionViewDto> AvailablePromotions { get; private set; } = [];


        public static ProductUnitViewDto Create(
            string publicId,
            string unitName,
            decimal originalPrice,
            decimal finalPrice,
            bool IsBaseUnit,
            List<PromotionViewDto> availablePromotions,
            string? barcode = null,
            decimal? discountValue = null
            )
        {

            return new ProductUnitViewDto
            {
                IsBaseUnit = IsBaseUnit,
                PublicId = publicId,
                UnitName = unitName,
                Barcode = barcode,
                OriginalPrice = originalPrice,
                FinalPrice = finalPrice,
                DiscountValue = discountValue,
                AvailablePromotions = availablePromotions
            };

        }
    }
}
