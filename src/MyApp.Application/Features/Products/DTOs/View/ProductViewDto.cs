using MyApp.Application.Features.Products.MapRaws;
using MyApp.Application.Features.ProductUints.DTOs.View;
using MyApp.Application.Features.ProductUints.MapRaws;
using MyApp.Application.Features.Promotions.DTOs;
using MyApp.Application.Features.Promotions.DTOs.View;
using MyApp.Application.Features.Promotions.LogicFeatures;
using MyApp.Application.Features.Promotions.MapRaws;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MyApp.Application.Features.Products.DTOs.View
{
    public class ProductViewDto
    {
        public string Name { get; private set; } = null!;
        public string Sku { get; private set; } = null!;
        public string? Barcode { get; private set; }
        public string Slug { get; private set; } = null!;
        public string BrandName { get; private set; } = null!;
        public int Status { get; private set; }
        public string StatusText { get; private set; } = null!;
        public string? ShortDescription { get; private set; }
        public string? Description { get; private set; }
        public string? PackingSize { get; private set; }
        public string? RegistrationNumber { get; private set; }
        public string? DosageForm { get; private set; }
        public string? Ingredient { get; private set; }
        public string? CategoryName { get; private set; }
        public List<string> Images { get; private set; } = new List<string>();
        public IReadOnlyList<ProductUnitViewDto> Units { get; private set; } = new List<ProductUnitViewDto>();

        /// <summary>
        /// Chuyển đổi từ RawDto sang Dto hiển thị cho Client
        /// </summary>
        public static ProductViewDto Create(ProductDetailRaw raw)
        {
            // Lấy thời điểm hiện tại một lần duy nhất để nhất quán trong toàn bộ quá trình map
            var currentTime = DateTimeOffset.UtcNow;

            return new ProductViewDto
            {
                Name = raw.Name,
                Slug = raw.Slug,
                Sku = raw.Sku,
                Status = raw.Status,
                StatusText = raw.StatusText,
                Barcode = raw.Barcode,
                BrandName = raw.BrandName,
                CategoryName = raw.CategoryName,
                ShortDescription = raw.ShortDescription,
                Description = raw.Description,
                PackingSize = raw.PackingSize,
                RegistrationNumber = raw.RegistrationNumber,
                DosageForm = raw.DosageForm,
                Ingredient = raw.Ingredient,
                Images = raw.Images,
                // Sử dụng lambda để truyền currentTime vào hàm xử lý unit
                Units = raw.Units.Select(u => BuildProductUnit(u, currentTime)).ToList()
            };
        }

        private static ProductUnitViewDto BuildProductUnit(ProductUnitRaw raw, DateTimeOffset now)
        {
            // 1. Khai báo các chiến lược (Trong thực tế nên inject qua Constructor)
            var strategies = new Dictionary<PromotionType, IPromotionStrategy> {
                            { PromotionType.Regular, new StandardStrategy() },
                            { PromotionType.FlashSale, new FlashSaleStrategy() }
                                     };

            decimal maxDiscount = 0;
            var validPromosForDisplay = new List<PromotionItemRaw>();

            // 2. Duyệt qua danh sách promo từ raw
            foreach (var p in raw.PromotionRawItems)
            {
                if (strategies.TryGetValue(p.Type, out var strategy) && strategy.CanApply(p, now))
                {
                    // Tính số tiền được giảm của item này
                    decimal currentDiscount = strategy.Calculate(raw.SellingPrice, p);

                    // Chỉ thêm vào danh sách hiển thị nếu nó thực sự có giảm giá
                    if (currentDiscount > 0)
                    {
                        validPromosForDisplay.Add(p);

                        // Cập nhật mức giảm lớn nhất
                        if (currentDiscount > maxDiscount)
                        {
                            maxDiscount = currentDiscount;
                        }
                    }
                }
            }

            // 3. Trả về đúng cấu trúc DTO ban đầu 
            return ProductUnitViewDto.Create(
                publicId: raw.PublicId.ToString(),
                unitName: raw.UnitName,
                originalPrice: raw.SellingPrice,
                finalPrice: raw.SellingPrice - maxDiscount,
                IsBaseUnit: raw.IsBaseUnit,
                // Giữ nguyên dữ liệu đầu ra khả dụng
                availablePromotions: validPromosForDisplay.Select(p => PromotionViewDto.From(p)).ToList(),
                barcode: raw.Barcode,
                discountValue: maxDiscount > 0 ? maxDiscount : null
            );
        }
    }
}
