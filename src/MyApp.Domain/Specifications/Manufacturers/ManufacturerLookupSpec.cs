using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Manufacturers
{
    public class ManufacturerLookupSpec : ManufacturerSpec
    {
        // Truyền cả object parameters vào để lấy Keyword và thông tin Phân trang
        public ManufacturerLookupSpec(ManufacturerParameters @params)
        {
                // 1. Lọc dữ liệu (Tự động không phân biệt dấu nhờ Collation trong DB)
                Criteria = m => string.IsNullOrEmpty(@params.KeySearch) ||
                                    m.Name.Contains(@params.KeySearch);
                                 
                // 2. Sắp xếp
                ApplyOrderBy(x => x.Id);
        }
     }
}
