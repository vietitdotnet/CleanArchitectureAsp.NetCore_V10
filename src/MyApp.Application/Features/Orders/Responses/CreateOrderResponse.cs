using MyApp.Application.Features.Orders.DTOs;
using MyApp.Application.Features.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Orders.Responses
{
    public class CreateOrderResponse
    {
        public  OrderDto Data { get; private set; } = null!;

        public CreateOrderResponse(OrderDto order)
        {
            Data = order;
        }
    }
}
