using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Orders.Requests
{
    public class CreateOrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
