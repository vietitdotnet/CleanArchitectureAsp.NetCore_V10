using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Enums
{
    public enum  ProductStatus
    {
        Draft = 0,        // đang tạo,chưa public
        Pending = 1,      // chờ duyệt
        Active = 2,       // đang bán
        Inactive = 3,     // tạm ngưng
        OutOfStock = 4,   // hết hàng
        Discontinued = 5, // ngừng kinh doanh
        Deleted = 6       // đã xóa mềm
    }
}
