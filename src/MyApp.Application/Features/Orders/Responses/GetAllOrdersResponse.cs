using MyApp.Application.Features.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Orders.Responses
{
    public class GetAllOrdersResponse
    {
        public IReadOnlyList<OrderDto> Data { get; private set; } = [];

        public GetAllOrdersResponse(IReadOnlyList<OrderDto> orders)
        {
            Data = orders;
        }
    }
}
