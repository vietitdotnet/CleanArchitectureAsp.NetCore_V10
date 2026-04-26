using MyApp.Application.Features.Orders.DTOs;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Orders.Responses
{
    public class GetOrdersResponse
    {
        public PagedResponse<OrderDto, OrderParameters> Orders { get; private set; }


        public GetOrdersResponse(PagedResponse<OrderDto, OrderParameters> orders)
        {
            Orders = orders;
        }
    }
}
