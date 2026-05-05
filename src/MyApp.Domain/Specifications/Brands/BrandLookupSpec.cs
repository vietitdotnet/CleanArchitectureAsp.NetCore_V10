using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Brands
{
    public class BrandLookupSpec : BrandSpec
    {
        public BrandLookupSpec(BrandParamesters @params)
        {
            // 1. Lọc dữ liệu (Tự động không phân biệt dấu nhờ Collation trong DB)
            Criteria = c => string.IsNullOrEmpty(@params.KeySearch) ||
                                c.Name.Contains(@params.KeySearch);

            // 2. Sắp xếp
            ApplyOrderBy(x => x.Id);
        }
    }
}
