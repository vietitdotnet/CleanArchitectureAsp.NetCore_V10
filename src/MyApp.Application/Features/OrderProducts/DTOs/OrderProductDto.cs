using MyApp.Application.Features.Products.DTOs;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.OrderProducts.DTOs
{
    public class OrderProductDto : BaseDto
    {
       
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
