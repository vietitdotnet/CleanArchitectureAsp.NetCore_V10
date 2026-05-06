using MyApp.Application.Features.ProductUints.DTOs;
using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Products.DTOs
{
    public class ProductDetailDto : BaseDto
    {
        public int Id { get; private set; }
        public string Sku { get; private set; } = null!;
        public string? ShortName { get; private set; }   
        public string? Barcode { get; private set; }
        public string Name { get; private set; } = null!;
        public decimal BasePrice { get; private set; }
        public decimal CostPrice { get; private set; }
        public string? BrandName { get; private set; }
        public string? PackingSize { get; private set; } // quy cách đóng gói
        public string? ShortDescription { get; private set; }// mô tả ngắn gọn về sản phẩm, có thể dùng để hiển thị trong danh sách sản phẩm
        public string? Description { get; private set; }// mô tả chi tiết về sản phẩm
        public string? RegistrationNumber { get; private set; }// số đăng ký, nếu có
        public string? DosageForm { get; private set; } // dạng bào chế 
        public string? Ingredient { get; private set; } // thành phần chính của sản phẩm, có thể dùng để hiển thị trong chi tiết sản phẩm
        public string? Benefit { get; private set; }
        public string? TaxName { get; private set; }
        public string? ManufacturerName { get; private set; }
        public string? CategoryName { get; private set; }
        public string TextStatus { get; private set; } = null!;
        public int Status { get; private set; }

        public IReadOnlyList<ProductUnitDetailDto> Units { get; private set; } = new List<ProductUnitDetailDto>();


    }
}
