using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.ProductUnits
{
    public class ProductUnitFilterSpec : ProductUnitSpec
    {
        // Truyền cả object parameters vào để lấy Keyword và thông tin Phân trang
        public ProductUnitFilterSpec(ProductUnitParameters @params)
        {
                // 1. Lọc dữ liệu (Tự động không phân biệt dấu nhờ Collation trong DB)
                Criteria = p => string.IsNullOrEmpty(@params.KeySearch) ||
                                    p.Product.Name.Contains(@params.KeySearch) ||
                                    p.UnitName.Contains(@params.KeySearch);

                // 2. Sắp xếp
                ApplyOrderBy(x => x.Id);
        }
     }
}
