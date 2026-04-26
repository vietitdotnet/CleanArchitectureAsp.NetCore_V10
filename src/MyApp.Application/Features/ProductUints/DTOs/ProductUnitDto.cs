using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.ProductUints.DTOs
{
    public class ProductUnitDto : BaseDto
    {
        public int Id { get; private set; }
        public Guid PublicId { get; private set; }

        public string ProductName { get; private set; } = null!;

        public int ProductId { get; private set; }

        public string? Barcode { get; private set; }

        public string UnitName { get; private set; } = null!;

        //Ví dụ: 1 thùng = 24 đơn vị base
        public int ConversionRate { get; private set; }

        public decimal SellingPrice { get; private set; }

        // Đơn vị gốc (chai, cái...)
        // Mặc định không phải đơn vị gốc ,
        // nếu là đơn vị gốc thì conversion rate = 1 ,
        // chỉ có 1 đơn vị gốc duy nhất cho mỗi sản phẩm
        public bool IsBaseUnit { get; private set; } = false;

        // Khi tạo mới sẽ không active ngay, phải đợi admin duyệt
        public bool IsActive { get; private set; } = false;

       

    }
}
