using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.FileImg
{
    public sealed class ProductImg : ObjectFolder
    {
        // Tên folder vật lý sẽ được tạo: ImageManager/Products
        public override string FolderName => "Products";

        // Bạn có thể ghi đè (override) các thông số mặc định nếu muốn
        // Ví dụ: Ảnh sản phẩm thường cần to hơn ảnh avatar
        public override int Size => 400;
        public override int Quality => 80;
    }
}
