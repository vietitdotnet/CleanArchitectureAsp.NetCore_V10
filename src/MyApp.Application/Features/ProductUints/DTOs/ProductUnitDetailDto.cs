using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.ProductUints.DTOs
{
    public class ProductUnitDetailDto : BaseDto
    {
        public int Id { get; private set; }

        public int ProductId { get; private set; }

        public string? Barcode { get; private set; }

        public string UnitName { get; private set; } = null!;

        public int ConversionRate { get; private set; }

        public decimal SellingPrice { get; private set; }

        public bool IsBaseUnit => ConversionRate == 1;

        // Khi tạo mới sẽ không active ngay, phải đợi admin duyệt
        public bool IsActive { get; private set; }
    }
}
