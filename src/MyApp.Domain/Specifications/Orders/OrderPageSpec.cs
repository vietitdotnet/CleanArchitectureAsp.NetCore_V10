using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Orders
{
    public class OrderPageSpec : OrderSpec
    {
        public OrderPageSpec(OrderParameters param)
        {
            // Tránh dùng trong kho lu trữ chung (BaseRepository)
            ApplyPaging((param.PageIndex - 1) * param.PageSize, param.PageSize);
        }
    }
}
