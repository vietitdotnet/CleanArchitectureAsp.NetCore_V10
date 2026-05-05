using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Products
{
    public class ProductLookupSpec : ProductSpec
    {
        public ProductLookupSpec(ProductParameters @params)
        {
            // 1. Lọc dữ liệu (Tự động không phân biệt dấu nhờ Collation trong DB)
            Criteria = p => string.IsNullOrEmpty(@params.KeySearch) ||
                            p.Name.Contains(@params.KeySearch) || 
                            p.Sku.Contains(@params.KeySearch) ||
                            (p.Barcode != null && p.Barcode.Contains(@params.KeySearch));
            // 2. Sắp xếp
            ApplyOrderBy(x => x.Id);
        }
    }
    
}
