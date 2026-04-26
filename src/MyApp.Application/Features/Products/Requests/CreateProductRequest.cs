using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyApp.Application.Features.Products.Requests
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "{0} không được bỏ trống...")]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
    }
}
