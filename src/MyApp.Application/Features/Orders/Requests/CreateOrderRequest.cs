using MyApp.Domain.Abstractions;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Orders.Requests
{
    public class CreateOrderRequest
    {
        public string? Note { get; set; }
        public string ProvinceCode { get; set; } = null!;
        public string CommuneCode { get; set; } = null!;
        public string AddressDetail { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; } = [];
    }
}
